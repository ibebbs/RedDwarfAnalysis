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

type TranscriptFormat =
| SameLine
| NextLine
| NextLineDoubleSpaced

type EpisodeSource = {
    Id : string
    Transcript : string option
    Format : TranscriptFormat
    Season : int
    Episode : int
}


let episodeSources = [
    { Id = "tt0684181"; Transcript = Some "Theend.txt"; Format = SameLine; Season = 1; Episode = 1 };
    { Id = "tt0684157"; Transcript = Some "Futureec.txt"; Format = SameLine; Season = 1; Episode = 2 };
    { Id = "tt0684145"; Transcript = Some "Balanceo.txt"; Format = SameLine; Season = 1; Episode = 3 };
    { Id = "tt0684186"; Transcript = Some "Waitingf.txt"; Format = SameLine; Season = 1; Episode = 4 };
    { Id = "tt0684151"; Transcript = Some "Confiden.txt"; Format = SameLine; Season = 1; Episode = 5 };
    { Id = "tt0684165"; Transcript = Some "Me2.txt"; Format = SameLine; Season = 1; Episode = 6 };
    { Id = "tt0684161"; Transcript = Some "Kryten.txt"; Format = SameLine; Season = 2; Episode = 1 };
    { Id = "tt0684146"; Transcript = Some "Betterth.txt"; Format = SameLine; Season = 2; Episode = 2 };
    { Id = "tt0684180"; Transcript = Some "Thanksfo.txt"; Format = SameLine; Season = 2; Episode = 3 };
    { Id = "tt0684177"; Transcript = Some "Stasisle.txt"; Format = SameLine; Season = 2; Episode = 4 };
    { Id = "tt0684175"; Transcript = Some "Queeg.txt"; Format = SameLine; Season = 2; Episode = 5 };
    { Id = "tt0684169"; Transcript = Some "Paralle.txt"; Format = SameLine; Season = 2; Episode = 6 };
    { Id = "tt0684144"; Transcript = Some "Backward.txt"; Format = SameLine; Season = 3; Episode = 1 };
    { Id = "tt0767232"; Transcript = Some "Marooned.txt"; Format = SameLine; Season = 3; Episode = 2 };
    { Id = "tt0684172"; Transcript = Some "Polymorp.txt"; Format = SameLine; Season = 3; Episode = 3 };
    { Id = "tt0684148"; Transcript = Some "Bodyswap.txt"; Format = SameLine; Season = 3; Episode = 4 };
    { Id = "tt0684185"; Transcript = Some "Timeslid.txt"; Format = SameLine; Season = 3; Episode = 5 };
    { Id = "tt0684183"; Transcript = Some "Thelastd.txt"; Format = SameLine; Season = 3; Episode = 6 };
    { Id = "tt0684149"; Transcript = Some "Camille.txt"; Format = SameLine; Season = 4; Episode = 1 };
    { Id = "tt0684152"; Transcript = Some "Dna.txt"; Format = SameLine; Season = 4; Episode = 2 };
    { Id = "tt0684160"; Transcript = Some "Justice.txt"; Format = SameLine; Season = 4; Episode = 3 };
    { Id = "tt0684187"; Transcript = Some "Whitehol.txt"; Format = SameLine; Season = 4; Episode = 4 };
    { Id = "tt0684153"; Transcript = Some "Dimensio.txt"; Format = SameLine; Season = 4; Episode = 5 };
    { Id = "tt0684164"; Transcript = Some "Meltdown.txt"; Format = SameLine; Season = 4; Episode = 6 };
    { Id = "tt0684159"; Transcript = Some "Holoship.txt"; Format = SameLine; Season = 5; Episode = 1 };
    { Id = "tt0684182"; Transcript = Some "Inquisit.txt"; Format = SameLine; Season = 5; Episode = 2 };
    { Id = "tt0684179"; Transcript = Some "Terrorfo.txt"; Format = SameLine; Season = 5; Episode = 3 };
    { Id = "tt0684174"; Transcript = Some "Quarinti.txt"; Format = SameLine; Season = 5; Episode = 4 };
    { Id = "tt0756588"; Transcript = Some "Demonsan.txt"; Format = SameLine; Season = 5; Episode = 5 };
    { Id = "tt0684143"; Transcript = Some "Backtore.txt"; Format = SameLine; Season = 5; Episode = 6 };
    { Id = "tt0684173"; Transcript = Some "Psirens.txt"; Format = SameLine; Season = 6; Episode = 1 };
    { Id = "tt0684163"; Transcript = Some "Legion.txt"; Format = SameLine; Season = 6; Episode = 2 };
    { Id = "tt0684158"; Transcript = Some "Gunmen.txt"; Format = SameLine; Season = 6; Episode = 3 };
    { Id = "tt0684155"; Transcript = Some "Emohawk.txt"; Format = SameLine; Season = 6; Episode = 4 };
    { Id = "tt0684176"; Transcript = Some "Rimmerwo.txt"; Format = SameLine; Season = 6; Episode = 5 };
    { Id = "tt0756589"; Transcript = Some "Outoftim.txt"; Format = SameLine; Season = 6; Episode = 6 };
    { Id = "tt0684184"; Transcript = Some "tikka.txt"; Format = NextLine; Season = 7; Episode = 1 };
    { Id = "tt0684178"; Transcript = Some "stoak.txt"; Format = NextLineDoubleSpaced; Season = 7; Episode = 2 };
    { Id = "tt0684168"; Transcript = Some "ouroboros.txt"; Format = NextLine; Season = 7; Episode = 3 };
    { Id = "tt0684154"; Transcript = Some "ductsoup.txt"; Format = NextLine; Season = 7; Episode = 4 };
    { Id = "tt0756587"; Transcript = Some "blue.txt"; Format = NextLineDoubleSpaced; Season = 7; Episode = 5 };
    { Id = "tt0684147"; Transcript = Some "beyond.txt"; Format = NextLineDoubleSpaced; Season = 7; Episode = 6 };
    { Id = "tt0684156"; Transcript = Some "epideme.txt"; Format = NextLineDoubleSpaced; Season = 7; Episode = 7 };
    { Id = "tt0684166"; Transcript = Some "nanarchy.txt"; Format = NextLineDoubleSpaced; Season = 7; Episode = 8 };
    { Id = "tt0684140"; Transcript = Some "bir1.txt"; Format = NextLine; Season = 8; Episode = 1 };
    { Id = "tt0684141"; Transcript = Some "bir2.txt"; Format = NextLine; Season = 8; Episode = 2 };
    { Id = "tt0684142"; Transcript = None; Format = NextLine; Season = 8; Episode = 3 };
    { Id = "tt0684150"; Transcript = Some "cassandra.txt"; Format = NextLine; Season = 8; Episode = 4 };
    { Id = "tt0684162"; Transcript = Some "krytietv.txt"; Format = NextLine; Season = 8; Episode = 5 };
    { Id = "tt0684170"; Transcript = Some "pete1.txt"; Format = NextLine; Season = 8; Episode = 6 };
    { Id = "tt0684171"; Transcript = Some "pete2.txt"; Format = NextLine; Season = 8; Episode = 7 };
    { Id = "tt0684167"; Transcript = Some "otg.txt"; Format = NextLine; Season = 8; Episode = 8 };
    { Id = "tt1365540"; Transcript = None; Format = NextLine; Season = 9; Episode = 1 };
    { Id = "tt1371606"; Transcript = None; Format = NextLine; Season = 9; Episode = 2 };
    { Id = "tt1400975"; Transcript = None; Format = NextLine; Season = 9; Episode = 3 };
    { Id = "tt1997038"; Transcript = None; Format = NextLine; Season = 10; Episode = 1 };
    { Id = "tt1999714"; Transcript = None; Format = NextLine; Season = 10; Episode = 2 };
    { Id = "tt1999715"; Transcript = None; Format = NextLine; Season = 10; Episode = 3 };
    { Id = "tt1999716"; Transcript = None; Format = NextLine; Season = 10; Episode = 4 };
    { Id = "tt1999717"; Transcript = None; Format = NextLine; Season = 10; Episode = 5 };
    { Id = "tt1999718"; Transcript = None; Format = NextLine; Season = 10; Episode = 6 };
    { Id = "tt5218244"; Transcript = None; Format = NextLine; Season = 11; Episode = 1 };
    { Id = "tt5218254"; Transcript = None; Format = NextLine; Season = 11; Episode = 2 };
    { Id = "tt5218266"; Transcript = None; Format = NextLine; Season = 11; Episode = 3 };
    { Id = "tt5218284"; Transcript = None; Format = NextLine; Season = 11; Episode = 4 };
    { Id = "tt5218308"; Transcript = None; Format = NextLine; Season = 11; Episode = 5 };
    { Id = "tt5218316"; Transcript = None; Format = NextLine; Season = 11; Episode = 6 }
]

type RatingsType = HtmlProvider< @"C:\Source\Repositories\RedDwarfAnalysis\Ratings\tt0684182.html" >
//let ratings = RatingsType.Load(@"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\Ratings\tt0684182.htm")

let ratingCategoryNames = [
  "Males";
  "Females";
  "Aged under 18";
  "Males under 18";
  "Aged 18-29";
  "Males Aged 18-29";
  "Females Aged 18-29";
  "Aged 30-44";
  "Males Aged 30-44";
  "Females Aged 30-44";
  "Aged 45+";
  "Males Aged 45+";
  "Females Aged 45+";
  "Top 1000 voters";
  "US users";
  "Non-US users";
]

type RatingCategory =
  | ``Males`` = 0
  | ``Females`` = 1
  | ``Aged under 18`` = 2
  | ``Males under 18`` = 3
  | ``Aged 18-29`` = 4
  | ``Males Aged 18-29`` = 5
  | ``Females Aged 18-29`` = 6
  | ``Aged 30-44`` = 7
  | ``Males Aged 30-44`` = 8
  | ``Females Aged 30-44`` = 9
  | ``Aged 45`` = 10
  | ``Males Aged 45`` = 11
  | ``Females Aged 45`` = 12
  | ``Top 1000 voters`` = 13
  | ``US users`` = 14
  | ``Non-US users`` = 15

type EpisodeRatings = {
    Id : string;
    Category : RatingCategory;
    Votes : int;
    Rating : decimal
}

let parseCategory c =
  let index = Seq.tryFindIndex (fun cn -> cn = c) ratingCategoryNames
  match index with
  | Some x -> Some (enum<RatingCategory>(x))
  | None -> None

let parseRatings id =
  let title (node : HtmlNode) =
      node.Descendants["a"]
      |> Seq.map (fun d -> d.InnerText())
  
  let votes (node : HtmlNode) =
      [ node.InnerText() ]
  
  let rating (node : HtmlNode) =
      [ node.InnerText() ]
  let document = HtmlDocument.Load(location + @"Ratings\" + id + ".html")
  let content = document.CssSelect("#tn15content").[0]
  let tables = 
    content.Descendants["table"]
    |> Seq.toArray
  let rows =
    tables.[1].Descendants["tr"]
    |> Seq.map (fun row -> (row, row.Descendants["td"] |> Seq.toArray))
    |> Seq.where (fun (row, data) -> data.Length = 3)
    |> Seq.map (fun (row, data) -> ( (title data.[0]), (votes data.[1]), (rating data.[2])))
    |> Seq.collect (fun (t, v, r) -> Seq.zip3 t v r)
    |> Seq.map (fun (t, v, r) -> ((parseCategory t), System.Int32.Parse(v.Trim()), System.Decimal.Parse(r.Trim())))
    |> Seq.where (fun (t, v, r) -> t.IsSome)
    |> Seq.map (fun (t, v, r) -> { Id = id; Category = t.Value; Votes = v; Rating = r })
  rows

type EpisodeRating = {
  ``Males`` : decimal option;
  ``Females`` : decimal option;
  ``Aged under 18`` : decimal option;
  ``Males under 18`` : decimal option;
  ``Aged 18-29`` : decimal option;
  ``Males Aged 18-29`` : decimal option;
  ``Females Aged 18-29`` : decimal option;
  ``Aged 30-44`` : decimal option;
  ``Males Aged 30-44`` : decimal option;
  ``Females Aged 30-44`` : decimal option;
  ``Aged 45`` : decimal option;
  ``Males Aged 45`` : decimal option;
  ``Females Aged 45`` : decimal option;
  ``Top 1000 voters`` : decimal option;
  ``US users`` : decimal option;
  ``Non-US users`` : decimal option;
}

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

let characterLine line =
    match line with
    | IsLister line -> Some (Lister, line)
    | IsRimmer line -> Some (Rimmer, line)
    | IsCat line -> Some (Cat, line)
    | IsKryten line -> Some (Kryten, line)
    | IsHolly line -> Some (Holly, line)
    | _ -> None
    
type CharacterLineSentiment = {
    Season : int
    Episode : int
    Character : Character 
    Line : string
    Sentiment : float }

let lineSentiment (line : string) =    
    printfn "Getting sentiment for line: %s" line
    let response = 
        Http.RequestString ( 
            "http://xi:9000/?properties={\"annotators\": \"tokenize,ssplit,sentiment\", \"date\": \"2017-04-02T15:13:00\", \"outputFormat\": \"json\"}&pipelineLanguage=en",
            headers = [ "Content-Type", "text/plain;;charset=UTF-8" ],
            httpMethod = "POST", 
            body = TextRequest line)

    let result = SentimentResponse.Parse(response);
    result.Sentences |> Seq.averageBy (fun s -> float (s.SentimentValue - 2))

type SameLineScanner = {
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

let parseSameLine (lines : string seq) =
    lines
    |> Seq.append (Seq.singleton "")
    |> Seq.scan SameLineScanner.Scan SameLineScanner.Empty
    |> Seq.collect (fun s -> s.Lines )
    |> Seq.map (fun l -> Regex.Replace(l, "\(.*?\)", "").Replace("_", "").Replace("*", "").Replace("  ", " ").Replace('"', '\'').Trim())
    |> Seq.where (fun l -> not (String.IsNullOrWhiteSpace(l)))

type NextLineScanner = {
    ReadingCharacter : Character option
    Spacing : int
    CurrentSpacing : int
    LineBuilder : string
    Lines : string seq
} with
    static member For spacing = { ReadingCharacter = None; Spacing = spacing; CurrentSpacing = 0; LineBuilder = ""; Lines = [] }
    static member Scan scanner (line : string) =
        match (line.ToUpper()) with
        | "LISTER" ->
            { scanner with ReadingCharacter = Some Lister; CurrentSpacing = 0; LineBuilder = ""; Lines = [ ] }
        | "RIMMER" ->
            { scanner with ReadingCharacter = Some Rimmer; CurrentSpacing = 0; LineBuilder = ""; Lines = [ ] }
        | "CAT" ->
            { scanner with ReadingCharacter = Some Cat; CurrentSpacing = 0; LineBuilder = ""; Lines = [ ] }
        | "KRYTEN" -> 
            { scanner with ReadingCharacter = Some Kryten; CurrentSpacing = 0; LineBuilder = ""; Lines = [ ] }
        | "HOLLY" ->
            { scanner with ReadingCharacter = Some Holly; CurrentSpacing = 0; LineBuilder = ""; Lines = [ ] }
        | _ ->
            match scanner.ReadingCharacter, String.IsNullOrWhiteSpace(line) with
            | Some character, false -> 
                { scanner with LineBuilder = (sprintf "%s %s" scanner.LineBuilder (line.Trim())); CurrentSpacing = 0; Lines = [] }
            | Some character, true when scanner.CurrentSpacing < scanner.Spacing -> 
                { scanner with CurrentSpacing = scanner.CurrentSpacing + 1; Lines = [] }
            | Some character, true -> 
                { scanner with ReadingCharacter = None; CurrentSpacing = 0; LineBuilder = ""; Lines = [ (sprintf "%s: %s" (characterName character) scanner.LineBuilder) ] }
            | _, _ -> 
                { scanner with ReadingCharacter = None; LineBuilder = ""; Lines = [ ] }

let parseNextLine (lines : string seq) spacing =
    lines
    |> Seq.append (Seq.singleton "")
    |> Seq.scan NextLineScanner.Scan (NextLineScanner.For spacing)
    |> Seq.collect (fun s -> s.Lines )
    |> Seq.map (fun l -> Regex.Replace(l, "\(.*?\)", "").Replace("_", "").Replace("*", "").Replace("  ", " ").Replace('"', '\'').Trim())
    |> Seq.where (fun l -> not (String.IsNullOrWhiteSpace(l)))

let results = 
    episodeSources
    |> Seq.where (fun s -> s.Transcript.IsSome)
    |> Seq.map (fun s -> (s.Season, s.Episode, s.Format, (File.ReadAllLines(Path.Combine(transcripts, s.Transcript.Value)))))
    |> Seq.collect (fun (season, episode, format, lines) -> 
        let parsedLines =
            match format with
            | SameLine -> parseSameLine lines
            | NextLine -> parseNextLine lines 0
            | NextLineDoubleSpaced -> parseNextLine lines 1
        parsedLines
        |> Seq.map (fun line -> (season, episode, line)))
    |> Seq.choose (fun (season, episode, line) ->
        let result = characterLine line
        match result with
        | Some (character, line) -> Some (season, episode, character, line)
        | None -> None)
    |> Seq.where (fun (season, episode, character, line) -> not (String.IsNullOrWhiteSpace(line)))
    |> Seq.map (fun (season, episode, character, line) -> 
        let sentiment = lineSentiment line
        (season, episode, character, line, sentiment))
    |> Seq.map (fun (season, episode, character, line, sentiment) -> sprintf "%i,%i,%s,%f,\"%s\"" season episode (characterName character) sentiment line)
    
File.WriteAllLines(Path.Combine(location, "Sentiment.csv"), results)

type SentimentType = CsvProvider<"Sentiment.csv", Schema = "Season,Episode,Character,Sentiment (float),Line (string)">
let sentiment= SentimentType.Load("Sentiment.csv")

let characters =
    sentiment.Rows
    |> Seq.groupBy (fun s -> s.Character)
    |> Seq.map (fun (character, lines) -> character)

    
    
episodeSources
|> Seq.where (fun s -> s.Transcript.IsSome && (s.Format = NextLine || s.Format = NextLineDoubleSpaced))
|> Seq.map (fun s -> (s.Season, s.Episode, s.Format, (File.ReadAllLines(Path.Combine(transcripts, s.Transcript.Value)))))
|> Seq.collect (fun (season, episode, format, lines) -> 
    let parsedLines =
        match format with
        | SameLine -> parseSameLine lines
        | NextLine -> parseNextLine lines 0
        | NextLineDoubleSpaced -> parseNextLine lines 1
    parsedLines
    |> Seq.map (fun line -> (season, episode, line)))   
|> Seq.choose (fun (season, episode, line) ->
    let result = characterLine line
    match result with
    | Some (character, line) -> Some (season, episode, character, line)
    | None -> None)
|> Seq.where (fun (season, episode, character, line) -> not (String.IsNullOrWhiteSpace(line))) 
|> Seq.iter (printfn "%A")



let characterSentimentChart = 
    sentiment.Rows
    |> Seq.groupBy (fun s -> s.Character)
    |> Seq.map (fun (character, lines) -> 
        lines 
        |> Seq.groupBy(fun l -> sprintf "S%sE%s" (l.Season.ToString("00")) (l.Episode.ToString("00"))) 
        |> Seq.map (fun (episode, l) -> (episode, l |> Seq.averageBy(fun l -> 2.0 / (l.Sentiment + 1.0)))))
    |> Chart.Line
    |> Chart.WithLabels characters

let sentimentChart = 
    sentiment.Rows
    |> Seq.groupBy(fun l -> sprintf "S%sE%s" (l.Season.ToString("00")) (l.Episode.ToString("00"))) 
    |> Seq.map (fun (episode, l) -> (episode, l |> Seq.averageBy(fun l -> l.Sentiment)))
    |> Chart.Line

sentimentChart |> Chart.Show

let ratingsChart =
    episodeSources
    |> Seq.collect (fun es -> 
        parseRatings es.Id
        |> Seq.where (fun r -> r.Category = RatingCategory.``Top 1000 voters``)
        |> Seq.map (fun r -> ( (sprintf "S%sE%s" (es.Season.ToString("00")) (es.Episode.ToString("00"))), r.Rating)))
    |> Chart.Line
    |> Chart.WithLabel "Rating"

ratingsChart |> Chart.Show

let episodeSentiment = 
    sentiment.Rows
    |> Seq.groupBy(fun l -> sprintf "S%sE%s" (l.Season.ToString("00")) (l.Episode.ToString("00"))) 
    |> Seq.map (fun (episode, l) -> (episode, ((l |> Seq.averageBy(fun l -> l.Sentiment)) + 1.0) / 2.0))
    |> Seq.toArray

sentiment.Rows |> Seq.maxBy (fun s -> s.Season)

let episodeRating =
    episodeSources
    |> Seq.where (fun s -> s.Transcript.IsSome)
    |> Seq.collect (fun es -> 
        parseRatings es.Id
        |> Seq.where (fun r -> r.Category = RatingCategory.``Top 1000 voters``)
        |> Seq.map (fun r -> ( (sprintf "S%sE%s" (es.Season.ToString("00")) (es.Episode.ToString("00"))), (float) r.Rating / 10.0)))
    |> Seq.toArray

[ episodeSentiment; episodeRating ]
|> Chart.Combo
|> Chart.WithOptions (Options(series = [| Series("lines"); Series("lines") |]))
|> Chart.WithLabels ["Sentiment"; "Rating"]
|> Chart.Show


    
lineSentiment
|> Seq.iter (printfn "%A")

lineSentiment
|> Chart.Combo
