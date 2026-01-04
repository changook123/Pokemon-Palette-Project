const fs = require('fs');

const allPokemon = [];

// 1~9세대 숫자와 'Special' 문자열을 포함한 배열 생성
const fileSuffixes = [1, 2, 3, 4, 5, 6, 7, 8, 9, 'Special'];

console.log("📂 데이터 병합을 시작합니다...\n");

fileSuffixes.forEach(suffix => {
    // 파일 이름 생성: Gen1.json ~ Gen9.json, 그리고 GenSpecial.json
    const fileName = `Gen${suffix}.json`;

    try {
        if (fs.existsSync(fileName)) {
            const data = JSON.parse(fs.readFileSync(fileName, 'utf-8'));
            allPokemon.push(...data); // 배열 합치기 (Spread Syntax)
            console.log(`✅ ${fileName} 병합 완료 (추가된 포켓몬: ${data.length}마리)`);
        } else {
            console.warn(`⚠️ 경고: '${fileName}' 파일을 찾을 수 없습니다.`);
        }
    } catch (e) {
        console.error(`❌ 에러: ${fileName} 읽기 실패 -`, e.message);
    }
});

// 하나의 큰 파일로 저장
const outputFileName = 'pokedex_total.json';
fs.writeFileSync(outputFileName, JSON.stringify(allPokemon, null, 2), 'utf-8');

console.log(`\n🎉 모든 작업 완료!`);
console.log(`총 [ ${allPokemon.length} ] 마리의 데이터가 '${outputFileName}'에 저장되었습니다.`);