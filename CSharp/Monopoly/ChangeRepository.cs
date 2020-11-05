using System.Collections.Generic;

namespace Monopoly
{
    public class ChangeRepository
    {
        private readonly List<Player> _players;

        public ChangeRepository(List<Player> players)
        {
            _players = players;
        }
        
        public ChanceStack GetStack()
        {
            var stack = new List<ChanceCard>
            {
                new EarnChangeCard(
                    50,
                    "Você acaba de receber uma parcela do seu 13º salário."),
                new PrisonExitCard(
                    "Saída livre da prisão. (Conserve este cartão para quando lhe for preciso ou negocie-o em qualquer ocasião, por preço a combinar."),
                new EarnChangeCard(
                    100,
                    "Seu cachorro policial tirou o 1º prêmio na exposição do Kennel Club."),
                new EarnChangeCard(
                    20,
                    "Você jogou na loteria esportiva com um grupo de amigos. Ganharam!"),
                new Earn50FromEveryOne(
                    "Você Apostou com os parceiros deste jogo e ganhou. Receba 50 de cada um.",
                    _players),
                new EarnChangeCard(
                    100,
                    "Você Foi promovido a diretor da sua empresa."),
                new EarnChangeCard(
                    150,
                    "Houve um assalto à sua loja, mas voce estava segurado."),
                new EarnChangeCard(
                    50,
                    "Você trocou seu carro usado com um amigo e ainda saiu lucrando."),
                new EarnChangeCard(
                    80,
                    "Um amigo tinha lhe pedido um empréstimo e se esqueceu de devolver. Ele acaba de se lembrar"),
                new EarnChangeCard(
                    200,
                    "Avance até o ponto de partida."),
                new EarnChangeCard(
                    45,
                    "Você Voce saiu de férias e se hospedou na casa de um amigo. Voce economizou o hotel"),
                new EarnChangeCard(
                    100,
                    "Você Tirou o primeiro lugar no torneio de Tênis do seu clube. Parabéns!"),
                new EarnChangeCard(
                    200,
                    "Você Esta com sorte. Suas ações na Bolsa de Valores estão em alta."),
                new EarnChangeCard(
                    25,
                    "A prefeitua mandou abrir uma nova avenida, para o que desapropriou vários prédios. Em consequencia, seu terreno valorizou."),
                new EarnChangeCard(
                    100,
                    "Inesperadamente, voce recebeu uma herança que já estava esquecida."),
                new SpendChangeCard(
                    100,
                    "Você é papai outra vez! Despesas de maternidade"),
                new SpendChangeCard(
                    45,
                    "O médico recomendou repouso num bom hote de montanha."),
                new SpendChangeCard(
                    15,
                    "Um amigo lhe pediu um empréstimo. Voce não pode recusar."),
                new SpendChangeCard(
                    25,
                    "Você Vai casar e está comprando um apartamento novo."),
                new SpendChangeCard(
                    25,
                    "Seu clube esta ampliando as piscinas. Os sócios devem contribuir."),
                new SpendChangeCard(
                    50,
                    "Seus filhos já vão para a escola. Pague a primeira mensalidade."),
                new SpendChangeCard(
                    50,
                    "Voce tem que pagar os bandidos do governo no imposto de renda."),
                new GoToPrisonChangeCard(
                    "Vá para a prisão sem receber nada. (talvez eu lhe faça uma visita...)"),
                new SpendChangeCard(
                    30,
                    "Voce achou interessante assistir à estréia da temporada de ballet. Compre os ingressos."),
                new SpendChangeCard(
                    30,
                    "Mais impostos! Renove a tempo a licença do seu automóvel."),
                new SpendChangeCard(
                    50,
                    "A geada prejudicou a sua safra de café."),
                new SpendChangeCard(
                    45,
                    "Seus parentes di interior vieram passar umas 'férias' na sua casa."),
                new SpendChangeCard(
                    100,
                    "Parabéns! Voce convidou seus amigos para festejar o aniversário."),
                new SpendChangeCard(
                    30,
                    "Mais estadismo ai! Voce estacionou seu carro em local proibido e ainda entrou na contra mão."),
                new SpendChangeCard(
                    50,
                    "Voce tem que pagar os bandidos do governo no imposto de renda.")
            };
           
            return (ChanceStack) stack.AsRandom();
        }
    }
}