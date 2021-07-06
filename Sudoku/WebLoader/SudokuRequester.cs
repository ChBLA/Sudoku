using Newtonsoft.Json;
using Sudoku.BaseGame.classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.WebLoader
{
    class SudokuRequester
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<String> getSudoku(int level)
        {
            var responseString = await client.GetStringAsync($"http://www.cs.utep.edu/cheon/ws/sudoku/new/?size=9&level={level}");
            return interpretResponse(responseString);
        }

        public String interpretResponse(String response)
        {
            const int ArraySize = 9;

            var responseVal = JsonConvert.DeserializeObject<EntryDef>(response);
            var valHolder = new int[ArraySize, ArraySize];
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    valHolder[i, j] = 0;
                }
            }

            foreach (var entry in responseVal.squares)
            {
                valHolder[entry.x, entry.y] = entry.value;
            }

            StringBuilder answer = new StringBuilder();

            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    answer.Append(valHolder[i, j]);
                }
            }

            return answer.ToString();
        }
    }
}
