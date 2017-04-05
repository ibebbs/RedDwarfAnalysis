#r "./packages/FSharp.Data.2.3.2/lib/net40/FSharp.Data.dll"
#r "./packages/XPlot.GoogleCharts.1.4.2/lib/net45/XPlot.GoogleCharts.dll"
#r "./packages/Google.DataTable.Net.Wrapper.3.1.2.0/lib/Google.DataTable.Net.Wrapper.dll"
#r "./packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll"
#r "./packages/XPlot.GoogleCharts.WPF.1.3.0/lib/net45/XPlot.GoogleCharts.WPF.dll"


//#r "./packages/FSharp.Charting.0.90.14/lib/net40/FSharp.Charting.dll"
//open FSharp.Charting;

open FSharp.Data
open System
open System.IO
open System.Text.RegularExpressions
open FSharp.Data.JsonExtensions
open XPlot
open XPlot.GoogleCharts
open XPlot.GoogleCharts.WpfExtensions

let location = @"C:\Source\Repositories\RedDwarfAnalysis\"
let transcripts = Path.Combine(location, "Transcript")

type SentimentResponse = JsonProvider<"SentimentResponse.json">

type EpisodeSource = {
    Id : string;
    Transcript : string option;
    Season : int
    Episode : int
}

let episodeSources = [
    { Id = "tt0684181"; Transcript = Some "Theend.txt"; Season = 1; Episode = 1 };
    { Id = "tt0684157"; Transcript = Some "Futureec.txt"; Season = 1; Episode = 2 };
    { Id = "tt0684145"; Transcript = Some "Balanceo.txt"; Season = 1; Episode = 3 };
    { Id = "tt0684186"; Transcript = Some "Waitingf.txt"; Season = 1; Episode = 4 };
    { Id = "tt0684151"; Transcript = Some "Confiden.txt"; Season = 1; Episode = 5 };
    { Id = "tt0684165"; Transcript = Some "Me2.txt"; Season = 1; Episode = 6 };
    { Id = "tt0684161"; Transcript = Some "Kryten.txt"; Season = 2; Episode = 1 };
    { Id = "tt0684146"; Transcript = Some "Betterth.txt"; Season = 2; Episode = 2 };
    { Id = "tt0684180"; Transcript = Some "Thanksfo.txt"; Season = 2; Episode = 3 };
    { Id = "tt0684177"; Transcript = Some "Stasisle.txt"; Season = 2; Episode = 4 };
    { Id = "tt0684175"; Transcript = Some "Queeg.txt"; Season = 2; Episode = 5 };
    { Id = "tt0684169"; Transcript = Some "Paralle.txt"; Season = 2; Episode = 6 };
    { Id = "tt0684144"; Transcript = Some "Backward.txt"; Season = 3; Episode = 1 };
    { Id = "tt0767232"; Transcript = Some "Marooned.txt"; Season = 3; Episode = 2 };
    { Id = "tt0684172"; Transcript = Some "Polymorp.txt"; Season = 3; Episode = 3 };
    { Id = "tt0684148"; Transcript = Some "Bodyswap.txt"; Season = 3; Episode = 4 };
    { Id = "tt0684185"; Transcript = Some "Timeslid.txt"; Season = 3; Episode = 5 };
    { Id = "tt0684183"; Transcript = Some "Thelastd.txt"; Season = 3; Episode = 6 };
    { Id = "tt0684149"; Transcript = Some "Camille.txt"; Season = 4; Episode = 1 };
    { Id = "tt0684152"; Transcript = Some "Dna.txt"; Season = 4; Episode = 2 };
    { Id = "tt0684160"; Transcript = Some "Justice.txt"; Season = 4; Episode = 3 };
    { Id = "tt0684187"; Transcript = Some "Whitehol.txt"; Season = 4; Episode = 4 };
    { Id = "tt0684153"; Transcript = Some "Dimensio.txt"; Season = 4; Episode = 5 };
    { Id = "tt0684164"; Transcript = Some "Meltdown.txt"; Season = 4; Episode = 6 };
    { Id = "tt0684159"; Transcript = Some "Holoship.txt"; Season = 5; Episode = 1 };
    { Id = "tt0684182"; Transcript = Some "Inquisit.txt"; Season = 5; Episode = 2 };
    { Id = "tt0684179"; Transcript = Some "Terrorfo.txt"; Season = 5; Episode = 3 };
    { Id = "tt0684174"; Transcript = Some "Quarinti.txt"; Season = 5; Episode = 4 };
    { Id = "tt0756588"; Transcript = Some "Demonsan.txt"; Season = 5; Episode = 5 };
    { Id = "tt0684143"; Transcript = Some "Backtore.txt"; Season = 5; Episode = 6 };
    { Id = "tt0684173"; Transcript = Some "Psirens.txt"; Season = 6; Episode = 1 };
    { Id = "tt0684163"; Transcript = Some "Legion.txt"; Season = 6; Episode = 2 };
    { Id = "tt0684158"; Transcript = Some "Gunmen.txt"; Season = 6; Episode = 3 };
    { Id = "tt0684155"; Transcript = Some "Emohawk.txt"; Season = 6; Episode = 4 };
    { Id = "tt0684176"; Transcript = Some "Rimmerwo.txt"; Season = 6; Episode = 5 };
    { Id = "tt0756589"; Transcript = Some "Outoftim.txt"; Season = 6; Episode = 6 };
    { Id = "tt0684184"; Transcript = Some "tikka.txt"; Season = 7; Episode = 1 };
    { Id = "tt0684178"; Transcript = Some "stoak.txt"; Season = 7; Episode = 2 };
    { Id = "tt0684168"; Transcript = Some "ouroboros.txt"; Season = 7; Episode = 3 };
    { Id = "tt0684154"; Transcript = Some "ductsoup.txt"; Season = 7; Episode = 4 };
    { Id = "tt0756587"; Transcript = Some "blue.txt"; Season = 7; Episode = 5 };
    { Id = "tt0684147"; Transcript = Some "beyond.txt"; Season = 7; Episode = 6 };
    { Id = "tt0684156"; Transcript = Some "epideme.txt"; Season = 7; Episode = 7 };
    { Id = "tt0684166"; Transcript = Some "nanarchy.txt"; Season = 7; Episode = 8 };
    { Id = "tt0684140"; Transcript = Some "bir1.txt"; Season = 8; Episode = 1 };
    { Id = "tt0684141"; Transcript = Some "bir2.txt"; Season = 8; Episode = 2 };
    { Id = "tt0684142"; Transcript = None; Season = 8; Episode = 3 };
    { Id = "tt0684150"; Transcript = Some "cassandra.txt"; Season = 8; Episode = 4 };
    { Id = "tt0684162"; Transcript = Some "krytietv.txt"; Season = 8; Episode = 5 };
    { Id = "tt0684170"; Transcript = Some "pete1.txt"; Season = 8; Episode = 6 };
    { Id = "tt0684171"; Transcript = Some "pete2.txt"; Season = 8; Episode = 7 };
    { Id = "tt0684167"; Transcript = Some "otg.txt"; Season = 8; Episode = 8 };
    { Id = "tt1365540"; Transcript = None; Season = 9; Episode = 1 };
    { Id = "tt1371606"; Transcript = None; Season = 9; Episode = 2 };
    { Id = "tt1400975"; Transcript = None; Season = 9; Episode = 3 };
    { Id = "tt1997038"; Transcript = None; Season = 10; Episode = 1 };
    { Id = "tt1999714"; Transcript = None; Season = 10; Episode = 2 };
    { Id = "tt1999715"; Transcript = None; Season = 10; Episode = 3 };
    { Id = "tt1999716"; Transcript = None; Season = 10; Episode = 4 };
    { Id = "tt1999717"; Transcript = None; Season = 10; Episode = 5 };
    { Id = "tt1999718"; Transcript = None; Season = 10; Episode = 6 };
    { Id = "tt5218244"; Transcript = None; Season = 11; Episode = 1 };
    { Id = "tt5218254"; Transcript = None; Season = 11; Episode = 2 };
    { Id = "tt5218266"; Transcript = None; Season = 11; Episode = 3 };
    { Id = "tt5218284"; Transcript = None; Season = 11; Episode = 4 };
    { Id = "tt5218308"; Transcript = None; Season = 11; Episode = 5 };
    { Id = "tt5218316"; Transcript = None; Season = 11; Episode = 6 }
]

type EpisodeLines = {
    Season : int
    Episode : int
    Lines : string[]
}

let (|IsLister|_|) (line : string) =
    match line.ToUpper().StartsWith("LISTER:") with
    | true when line.Length > 8 -> Some (line.Substring(8))
    | _ -> None

let (|IsKryten|_|) (line : string) =
    match line.ToUpper().StartsWith("KRYTEN:") with
    | true when line.Length > 8 -> Some (line.Substring(8))
    | _ -> None

let (|IsRimmer|_|) (line : string) =
    match line.ToUpper().StartsWith("RIMMER:") with
    | true when line.Length > 8 -> Some (line.Substring(8))
    | _ -> None

let (|IsCat|_|) (line : string) =
    match line.ToUpper().StartsWith("CAT:") with
    | true when line.Length > 5 -> Some (line.Substring(5))
    | _ -> None

let (|IsHolly|_|) (line : string) =
    match line.ToUpper().StartsWith("HOLLY:") with
    | true when line.Length > 7 -> Some (line.Substring(7))
    | _ -> None

type Character =
| Lister
| Rimmer
| Cat
| Kryten
| Holly

let characterName character =
    match character with
    | Lister -> "LISTER"
    | Rimmer -> "RIMMER"
    | Cat -> "CAT"
    | Kryten -> "KRYTEN"
    | Holly -> "HOLLY"


type EpisodeText = {
    Season : int
    Episode : int
    Text : string 
}

type CharacterLine = {
    Season : int
    Episode : int
    Character : Character 
    Line : string 
}

let characterLine text =
    match text.Text with
    | IsLister line -> Some { Season = text.Season; Episode = text.Episode; Character = Lister; Line = line }
    | IsRimmer line -> Some { Season = text.Season; Episode = text.Episode; Character = Rimmer; Line = line }
    | IsCat line -> Some { Season = text.Season; Episode = text.Episode; Character = Cat; Line = line }
    | IsKryten line -> Some { Season = text.Season; Episode = text.Episode; Character = Kryten; Line = line }
    | IsHolly line -> Some { Season = text.Season; Episode = text.Episode; Character = Holly; Line = line }
    | _ -> None
    
type CharacterLineSentiment = {
    Season : int
    Episode : int
    Character : Character 
    Line : string
    Sentiment : float }

let lineSentiment (line : CharacterLine) =    
    printfn "Getting sentiment for %s's line: %s" (line.Character.ToString()) line.Line
    let response = 
        Http.RequestString ( 
            "http://xi:9000/?properties={\"annotators\": \"tokenize,ssplit,sentiment\", \"date\": \"2017-04-02T15:13:00\", \"outputFormat\": \"json\"}&pipelineLanguage=en",
            headers = [ "Content-Type", "text/plain;;charset=UTF-8" ],
            httpMethod = "POST", 
            body = TextRequest line.Line)

    let result = SentimentResponse.Parse(response);
    let sentiment = result.Sentences |> Seq.averageBy (fun s -> float (s.SentimentValue - 2))
    { Season = line.Season; Episode = line.Episode; Character = line.Character; Line = line.Line; Sentiment = sentiment}

type LineScanner = {
    LineBuilder : string
    Lines : string seq
} with
    static member Empty = { LineBuilder = ""; Lines = [] }
    static member Scan scanner (line : string) =
        match line.StartsWith("  ") with
        | true -> 
            let result = sprintf "%s %s" scanner.LineBuilder (line.Trim())
            { scanner with LineBuilder = result; Lines = [] }
        | false -> 
            { scanner with LineBuilder = line; Lines = [ scanner.LineBuilder ]}

let results = 
    episodeSources
    |> Seq.where (fun s -> s.Transcript.IsSome)
    |> Seq.map (fun s -> { Season = s.Season; Episode = s.Episode; Lines = (File.ReadAllLines(Path.Combine(transcripts, s.Transcript.Value))) })
    |> Seq.collect (fun lines -> 
        lines.Lines
        |> Seq.append (Seq.singleton "")
        |> Seq.scan LineScanner.Scan LineScanner.Empty
        |> Seq.collect (fun s -> s.Lines )
        |> Seq.map (fun l -> Regex.Replace(l, "\(.*?\)", "").Replace("_", "").Replace("  ", " ").Trim())
        |> Seq.where (fun l -> not (String.IsNullOrWhiteSpace(l)))
        |> Seq.map (fun l -> { Season = lines.Season; Episode = lines.Episode; Text = l }))
    |> Seq.choose (fun et -> (characterLine et))
    |> Seq.where (fun cl -> not (String.IsNullOrWhiteSpace(cl.Line)))
    |> Seq.map (fun cl -> (lineSentiment cl))
    |> Seq.map (fun ls -> sprintf "%i,%i,%s,%f,\"%s\"" ls.Season ls.Episode (characterName ls.Character) ls.Sentiment ls.Line)

File.WriteAllLines(Path.Combine(location, "Sentiment.csv"), results)

Regex.Replace("CAT: (Laughs.)", "\(.*?\)", "").Replace("_", "").Replace("  ", " ").Trim()