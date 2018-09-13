using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class BotPergunta
    {
        const string CHANNEL_PERGUNTAS = "perguntas";
        private readonly IDatabase _db;
        private readonly ISubscriber _pub;

        public BotPergunta(string connectionString)
        {
            var client = ConnectionMultiplexer.Connect(connectionString);
            var db = client.GetDatabase();
            var pub = client.GetSubscriber();

            this._db = db;
            this._pub = pub;
        }

        public void Perguntar(int id, string texto)
        {
            string pergunta = $"P{id}: {texto}";
            _pub.Publish(CHANNEL_PERGUNTAS, pergunta);
        }

        public async Task<Resposta[]> EsperarRespostasAsync(int id)
        {
            // espera 1 seg
            await Task.Delay(1000);

            var respostas = LerRespostas(id);
            if(respostas.Length > 0)
            {
                return respostas;
            }

            await Task.Delay(8000);
            respostas = LerRespostas(id);

            return respostas;
        }

        private Resposta[] LerRespostas(int id)
        {
            string perguntaId = "P" + id.ToString();

            var respostas = _db.HashGetAll(perguntaId)
                                .Select(entry => new Resposta
                                {
                                    Autor = entry.Name,
                                    Texto = entry.Value
                                })
                                .ToArray();

            return respostas;
        }
    }
}
