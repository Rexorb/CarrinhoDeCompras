//Função global interna do C# para corrigir acentuação, linguagem localizada e formatada para UTF8
Console.OutputEncoding = System.Text.Encoding.UTF8;

//Variáveis de armazemanento, de início optei por utilizar array, mas depois de algumas pesquisas conclui que List seria melhor para manipular, especialmente em estados
//carrinhoItens armazena o atual estado do carrinho de compras
//carinhoPrecos armazena os preços dos itens contidos no carrinhoItens
List<string> carrinhoItens = new List<string>();
List<double> carrinhoPrecos = new List<double>();

//Abaixo dois vetores/arrays para armazenar o catálogo de compras, o estoqueItens responsável para armazenar os nomes enquanto precos armazena os preços
string[] estoqueItens = { "Camiseta P", "Camiseta M", "Camiseta G", "Camiseta GG" };
double[] precos = { 40.00, 45.00, 50.00, 55.00 };

bool executando = true;

//O menu principal, sendo executado em uma estrutura de repetição, onde é possível que o usuário consiga facilmente navegar apenas digitando de 1 a 5
while (executando)
{
    Console.WriteLine("\n=== Menu Principal ===");
    Console.WriteLine("1 - Consultar produtos");
    Console.WriteLine("2 - Adicionar item ao carrinho");
    Console.WriteLine("3 - Ver carrinho");
    Console.WriteLine("4 - Limpar carrinho");
    Console.WriteLine("5 - Finalizar compra");
    Console.WriteLine("6 - Sair");
    Console.Write("Digite sua opção: ");
    //Variável string para armazenar a opção digitada pelo usuário
    //Importante, estou ciente de que poderia ter utilizado um int para armazenagem, mas isso levaria mais linhas de código para fazer as tratativas caso o usuário
    //entrasse com um valor que não fosse int
    string input = Console.ReadLine()!;
    //Estrutura de decisão juntamente com um switch para aplicar a funcionalidade do menu
    if (int.TryParse(input, out int opcao))
    {
        switch (opcao)
        {
            case 1: // Consultar produtos -> Aqui o usuário consegue consultar quais produtos estão disponíveis no menu
                ExibirTabelaProdutos();
                break;

            case 2: // Adicionar ao carrinho -> Aqui o usuário consegue adicionar ao carrinho os itens listados no menu
                AdicionarAoCarrinho();
                break;

            case 3: // Ver carrinho -> Aqui o usuário consegue listar quais os produtos que estão atualmente armazenados na variável carrinhoItens
                VerCarrinho();
                break;

            case 4: // Limpar carrinho -> Aqui o usuário pode limpar o carrinho caso queira, excluindo todos os itens contidos nele
                LimparCarrinho();
                break;

            case 5: // Finalizar compra -> Aqui o usuário finaliza a compra e o programa se encerra
                FinalizarCompra();
                executando = false;
                break;

            case 6: // Sair -> O programa se encerra sem quaisquer interações
                executando = false;
                break;
            //Meio desnecessário, porém para desencargo do case, optei por deixar o método default seguido da mensagem de "Opção Inválida"
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
    //Aqui caso o usuário digitar algo que não for entre 1 a 5, o sistema retorna para digitar apenas um valor válido
    else
    {
        Console.WriteLine("Digite apenas números válidos!");
    }
}

// Métodos auxiliares = Para evitar boilerplate e melhorar a organização, optei por fazer funções específicas ao final do código

//Função que exibe em tabela formatada a lista de cada produto de acordo com o exercício
void ExibirTabelaProdutos()
{
    Console.WriteLine("\nCatálogo de Produtos:");
    Console.WriteLine(@"+--------+-----------------+----------------+
| Código | Descrição       | Valor unitário |
+--------+-----------------+----------------+
| 1      | Camiseta P      | R$40,00        |
| 2      | Camiseta M      | R$45,00        |
| 3      | Camiseta G      | R$50,00        |
| 4      | Camiseta GG     | R$55,00        |
+--------+-----------------+----------------+");
}
//Função para adicionar o produto escolhido pelo usuário ao carrinho
void AdicionarAoCarrinho()
{
    ExibirTabelaProdutos();
    Console.Write("\nDigite o código do produto que deseja adicionar: ");
    //Estrutura de decisão para identificar qual item do catálogo o usuário selecionar com base nos códigos: 1 -> Camisa P | 2 -> Camisa M | 3 -> Camisa G | 4 -> Camisa GG
    if (int.TryParse(Console.ReadLine(), out int codigo) && codigo >= 1 && codigo <= 4)
    {
        //Caso selecionado, é adicionado em ambas variáveis <List>
        carrinhoItens.Add(estoqueItens[codigo - 1]);
        carrinhoPrecos.Add(precos[codigo - 1]);
        Console.WriteLine($"\n✅ {estoqueItens[codigo - 1]} adicionado ao carrinho!");
    }
    else
    {
        Console.WriteLine("Código inválido! Use valores entre 1 e 4.");
    }
}

void VerCarrinho()
{
    if (carrinhoItens.Count == 0)
    {
        Console.WriteLine("\n🛒 Seu carrinho está vazio!");
        return;
    }

    Console.WriteLine("\n🛒 Conteúdo do Carrinho:");
    Console.WriteLine("+----+-----------------+--------------+");
    Console.WriteLine("| #  | Item            | Valor        |");
    Console.WriteLine("+----+-----------------+--------------+");

    //Estrutura de repetição para percorrer a <List> da variável carrinhoItens e carrinhoPrecos e então aninhar o Item com o Valor respectivo com base na compra do usuário
    double total = 0;
    for (int i = 0; i < carrinhoItens.Count; i++)
    {
        Console.WriteLine($"| {i + 1,-2} | {carrinhoItens[i],-15} | R${carrinhoPrecos[i],-10:F2} |");
        total += carrinhoPrecos[i];
    }

    Console.WriteLine("+----+-----------------+--------------+");
    Console.WriteLine($"| Total: {total,30:F2} |");
    Console.WriteLine("+-------------------------------+");
}

void LimparCarrinho()
{
    Console.WriteLine("\nDeseja limpar o carrinho ? Digite:");
    Console.WriteLine("1 - Limpar o carrinho");
    Console.WriteLine("2 - Manter o carrinho e voltar ao menu");
    string inputLimpa = Console.ReadLine()!;

    if (inputLimpa == "1")
    {
        carrinhoItens.Clear();
        carrinhoPrecos.Clear();
        Console.WriteLine("\n✅ Carrinho limpo!");
    }
    else if (inputLimpa == "2")
    {
        Console.WriteLine("\n Carrinho mantido.");
        return;
    }
    else
    {
        Console.WriteLine("Erro, insira uma opção válida");
    }
}
//Função para encerrar o programa com uma estrutura simples de decisão, caso haja itens no carrinho o sistema finalizará com agradecimentos.
//Caso o carrinho esteja vazio o sistema finalizará retornando que a compra não foi realizada
void FinalizarCompra()
{
    if (carrinhoItens.Count == 0)
    {
        Console.WriteLine("\n❌ Compra não realizada - Carrinho vazio!");
        return;
    }

    VerCarrinho();
    Console.WriteLine("\n✅ Compra finalizada com sucesso!");
    Console.WriteLine("Obrigado pela preferência!");
}