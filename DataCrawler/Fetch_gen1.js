const axios = require('axios');
const fs = require('fs');

// 세대별 도감 번호 범위 (1~9세대)
const generations = [
    { gen: 1, start: 1, end: 151 },
    { gen: 2, start: 152, end: 251 },
    { gen: 3, start: 252, end: 386 },
    { gen: 4, start: 387, end: 493 },
    { gen: 5, start: 494, end: 649 },
    { gen: 6, start: 650, end: 721 },
    { gen: 7, start: 722, end: 809 },
    { gen: 8, start: 810, end: 905 },
    { gen: 9, start: 906, end: 1025 }
];

// 🚫 다이맥스 불가능 목록 (자시안, 자마젠타, 무한다이노) -> 무한다이노(890)는 보스용으로 허용!
const noDynamaxIds = [888, 889]; 

// 포켓몬 데이터 1개 상세 조회 함수
async function fetchPokemonData(id, url) {
    try {
        const mainRes = await axios.get(`https://pokeapi.co/api/v2/pokemon/${id}`);
        const mainData = mainRes.data;

        // 폼체인지/특수폼은 species 정보가 원본과 연결되어 있습니다.
        // mainData.species.url을 통해 원본의 한글 이름을 가져옵니다.
        const speciesRes = await axios.get(mainData.species.url);
        const speciesData = speciesRes.data;

        // 한글 이름 찾기
        const krNameObj = speciesData.names.find(n => n.language.name === 'ko');
        let krName = krNameObj ? krNameObj.name : mainData.name;

        // 특수 폼의 경우 이름 뒤에 구분을 위해 영어 폼 이름을 붙여줌 (중복 방지)
        // 예: 리자몽(charizard-mega-y)
        if (id > 10000) {
            krName = `${krName} (${mainData.name.replace(speciesData.name + '-', '')})`;
        }

        // 기믹 판별 로직
        const hasMegaEvolution = speciesData.varieties.some(v => v.pokemon.name.includes('-mega'));
        const hasGigantamax = speciesData.varieties.some(v => v.pokemon.name.includes('-gmax') || v.pokemon.name.includes('-eternamax'));
        
        // 다이맥스는 기본적으로 모두 가능하지만, 금지 목록에 있거나 특정 조건이면 제외
        // (특수 폼들은 다이맥스 안되는 경우가 많지만 일단 열어둡니다)
        const canDynamax = !noDynamaxIds.includes(mainData.id);

        return {
            id: mainData.id,
            name: krName,
            types: mainData.types.map(t => t.type.name),
            baseStats: {
                hp: mainData.stats[0].base_stat,
                atk: mainData.stats[1].base_stat,
                def: mainData.stats[2].base_stat,
                spAtk: mainData.stats[3].base_stat,
                spDef: mainData.stats[4].base_stat,
                speed: mainData.stats[5].base_stat
            },
            abilities: mainData.abilities.map(a => a.ability.name),
            gimmickFlags: {
                canMega: hasMegaEvolution,
                canTerastal: true,
                canDynamax: canDynamax,
                canGigantamax: hasGigantamax,
                canZAttack: true // 변수명 변경 적용 완료
            }
        };

    } catch (error) {
        console.error(`❌ ID ${id} 실패:`, error.message);
        return null;
    }
}

async function main() {
    console.log("=== 📡 포켓몬 전체 리스트 조회 중... ===");
    
    // 1. 존재하는 모든 포켓몬 리스트를 한 번에 가져옵니다 (limit을 크게 설정)
    const listRes = await axios.get('https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0');
    const allPokemon = listRes.data.results;
    
    console.log(`✅ 총 ${allPokemon.length}마리의 포켓몬 데이터를 발견했습니다.`);

    // 2. ID를 추출해서 분류 (URL에서 ID를 파싱)
    const specialForms = [];
    
    // 일반 도감 번호(1~1025)는 기존 방식대로 세대별 처리하고,
    // 10001번 이상인 애들만 골라냅니다.
    allPokemon.forEach(p => {
        const id = parseInt(p.url.split('/').filter(Boolean).pop());
        if (id > 10000) {
            specialForms.push({ id, url: p.url });
        }
    });

    // 3. 일반 세대별 데이터 수집 (기존 로직 유지)
    for (const genInfo of generations) {
        const { gen, start, end } = genInfo;
        const pokemonList = [];
        console.log(`\n🚀 [Gen ${gen}] 데이터 수집 시작 (${start}~${end})...`);

        // 병렬 처리를 위해 chunk 단위로 끊어서 요청 (속도 향상 및 에러 방지)
        for (let i = start; i <= end; i++) {
            const data = await fetchPokemonData(i);
            if (data) pokemonList.push(data);
            if (i % 20 === 0) process.stdout.write(`.`); // 진행바 느낌
        }
        
        fs.writeFileSync(`Gen${gen}.json`, JSON.stringify(pokemonList, null, 2), 'utf-8');
        console.log(`\n✅ Gen${gen}.json 저장 완료!`);
    }

    // 4. 특수 폼(10001~) 데이터 수집
    console.log(`\n🚀 [Special Forms] 특수 폼 데이터 수집 시작 (총 ${specialForms.length}개)...`);
    const specialList = [];
    
    for (let i = 0; i < specialForms.length; i++) {
        const item = specialForms[i];
        const data = await fetchPokemonData(item.id);
        
        if (data) {
            specialList.push(data);
            console.log(`   [${i + 1}/${specialForms.length}] ${data.name}`);
        }
    }

    fs.writeFileSync(`GenSpecial.json`, JSON.stringify(specialList, null, 2), 'utf-8');
    console.log(`\n✅ GenSpecial.json 저장 완료! 모든 작업이 끝났습니다! 🎉`);
}

main();