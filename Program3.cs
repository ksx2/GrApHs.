
using GraphT3.BridgesAndHinges;
using GraphBase.IO;
using GraphBase.Objects;


try
{
    var input = new Input(args);
    var output = new Output(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var graph = new Graph(input.GetFactory());
    graph.Print();
    var bridgeAndHingesSeeker = new BridgesAndHinges(graph);

    output.WriteLine("Мосты в графе: ");
    var bridges = bridgeAndHingesSeeker.GetBridges();
    output.WriteLine(bridgeAndHingesSeeker.ConvertToString(bridges));

    output.WriteLine("Шарниры в графе: ");
    var hinges = bridgeAndHingesSeeker.GetHinges();
    output.WriteLine(bridgeAndHingesSeeker.ConvertToString(hinges));

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


