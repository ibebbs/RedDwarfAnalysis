#r "./packages/FSharp.Data.2.3.2/lib/net40/FSharp.Data.dll"
#r "./packages/XPlot.GoogleCharts.1.4.2/lib/net45/XPlot.GoogleCharts.dll"
#r "./packages/Google.DataTable.Net.Wrapper.3.1.2.0/lib/Google.DataTable.Net.Wrapper.dll"
#r "./packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll"
#r "./packages/XPlot.GoogleCharts.WPF.1.3.0/lib/net45/XPlot.GoogleCharts.WPF.dll"


//#r "./packages/FSharp.Charting.0.90.14/lib/net40/FSharp.Charting.dll"
//open FSharp.Charting;

open FSharp.Data;
open System.IO;
open FSharp.Data.JsonExtensions;
open XPlot;
open XPlot.GoogleCharts;
open XPlot.GoogleCharts.WpfExtensions;

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

let writeFile path id (json : string) =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\" + path + @"\" + id + ".json"
  use streamWriter = new StreamWriter(fileName, false)
  streamWriter.WriteLine(json)

// Using: http://www.omdbapi.com/
episodeSources
|> Seq.map (fun es -> (es.Id,  Http.RequestString ( "http://www.omdbapi.com/", query=["i", es.Id; "tomatoes", "true"] )))
|> Seq.iter (fun (id, json) -> writeFile "Omdb" id json)

// Using: http://imdb.wemakesites.net/
episodeSources
|> Seq.map (fun es -> (es.Id,  Http.RequestString ( "http://imdb.wemakesites.net/api/" + es.Id, query=["api_key", "5ff83107-8872-4324-902a-283fc7626a38"] )))
|> Seq.iter (fun (id, json) -> writeFile "WeMakeSites" id json)

// Using: http://api.themoviedb.org/
episodeSources
|> Seq.map (fun es -> System.Threading.Thread.Sleep(1000); es)
|> Seq.map (fun es -> (es.Id, Http.RequestString ("https://api.themoviedb.org/3/tv/326/season/" + es.Season.ToString() + "/episode/" + es.Episode.ToString(), query=["api_key", "1b9b9807adc4f6af36df49c81b20f0fc"])))
|> Seq.iter (fun (id, json) -> writeFile "TheMovieDb" id json)

type OmdbRecord = JsonProvider< @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\Omdb\tt0684181.json" >
type WeMakeSitesRecord = JsonProvider< @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\WeMakeSites\tt0684181.json" >
type TheMoviewDbRecord = JsonProvider< @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\TheMovieDb\tt0684181.json" >

type RatingsType = HtmlProvider< @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\Ratings\tt0684182.htm" >
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

let parseRatings id =
  let title (node : HtmlNode) =
      node.Descendants["a"]
      |> Seq.map (fun d -> d.InnerText())
  
  let votes (node : HtmlNode) =
      [ node.InnerText() ]
  
  let rating (node : HtmlNode) =
      [ node.InnerText() ]
  let document = HtmlDocument.Load(@"http://www.imdb.com/title/" + id + "/ratings")
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
    |> Seq.map (fun (t, v, r) -> ((enum<RatingCategory>(Seq.findIndex (fun cn -> cn = t) ratingCategoryNames)), System.Int32.Parse(v.Trim()), System.Decimal.Parse(r.Trim())))
    |> Seq.map (fun (t, v, r) -> { Id = id; Category = t; Votes = v; Rating = r })
  rows

parseRatings "tt1997038"
|> Seq.where (fun r -> r.Category = RatingCategory.``Aged under 18`` || r.Category = RatingCategory.``Aged 18-29`` || r.Category = RatingCategory.``Aged 30-44`` || r.Category = RatingCategory.``Aged 45``)
|> Seq.iter (fun r -> printfn "Type %A, Votes %i, Rating %f" r.Category r.Votes r.Rating)
  

let record = TheMoviewDbRecord.Load( @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\TheMovieDb\tt0684181.json" )


let loadOmdbFile id =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\Omdb\" + id + ".json"
  OmdbRecord.Load(fileName) 
  
let loadWeMakeSitesFile id =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\WeMakeSites\" + id + ".json"
  WeMakeSitesRecord.Load(fileName) 
  
let loadTheMovieDbFile id =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\TheMovieDb\" + id + ".json"
  WeMakeSitesRecord.Load(fileName) 

let loadFiles id =
  let omdbFile = loadOmdbFile id
  let wmsFile = loadWeMakeSitesFile id
  (omdbFile, wmsFile)

let loadOmdbJson id =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\Omdb\" + id + ".json"
  JsonValue.Load(fileName) 
  
let loadWeMakeSitesJson id =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\WeMakeSites\" + id + ".json"
  JsonValue.Load(fileName) 
  
let loadTheMovieDbJson id =
  let fileName = @"E:\Source\Repositories\Spikes\Spikes\RedDwarfAnalysis\TheMovieDb\" + id + ".json"
  JsonValue.Load(fileName) 

let loadJson id =
  let omdbFile = loadOmdbJson id
  let wmsFile = loadWeMakeSitesJson id
  let mdbFile = loadTheMovieDbJson id
  (omdbFile, wmsFile, mdbFile)

episodeSources
|> Seq.map (fun es -> loadJson es.Id)
|> Seq.map (fun (omdb, wms, mdb) -> (wms?data?released, omdb?Title, omdb?imdbRating))
|> Seq.where (fun (date, title, rating) -> not (rating.AsString() = "N/A"))
|> Seq.sortBy (fun (date, title, rating) -> date)
|> Seq.iter (fun (date, title, rating) -> printfn "Date: %A, Title: %s, Rating: %f" (date.AsDateTime()) (title.AsString()) (rating.AsDecimal()))


// Generates very wide line chart due to large time distance between seasons
let ratingsByDate = 
  episodeSources
  |> Seq.map (fun es -> loadJson es.Id)
  |> Seq.map (fun (omdb, wms, mdb) -> (wms?data?released, omdb?Title, omdb?imdbRating))
  |> Seq.where (fun (date, title, rating) -> not (rating.AsString() = "N/A"))
  |> Seq.map (fun (date, title, rating) -> (date.AsDateTime(), title.AsString(), rating.AsDecimal()))
  |> Seq.sortBy (fun (date, title, rating) -> date)
  
//Chart.Line((ratingsByDate |> Seq.map (fun (date, title, rating) -> (date, rating))), "Episodes", "Episodes", (ratingsByDate |> Seq.map (fun (date, title, rating) -> title))) |> Chart.Show

let options = Options(pointSize=3, colors=[|"#3B8FCC"|], trendlines=[|Trendline(opacity=0.5,lineWidth=10,color="#C0D9EA")|], hAxis=Axis(title="Date"), vAxis=Axis(title="Rating"))

Chart.Scatter((ratingsByDate |> Seq.map (fun (date, title, rating) -> (date, rating))))
|> Chart.WithOptions(options)
|> Chart.Show

let maxRating (episodes : seq<System.DateTime * string * int * decimal>) =
  episodes
  |> Seq.map (fun (date, title, season, rating) -> rating)
  |> Seq.max

let minRating (episodes : seq<System.DateTime * string * int * decimal>) =
  episodes
  |> Seq.map (fun (date, title, season, rating) -> rating)
  |> Seq.min

let avgRating (episodes : seq<System.DateTime * string * int * decimal>) =
  episodes
  |> Seq.map (fun (date, title, season, rating) -> rating)
  |> Seq.average

let ratingsBySeason = 
  episodeSources
  |> Seq.map (fun es -> loadJson es.Id)
  |> Seq.map (fun (omdb, wms, mdb) -> (wms?data?released, omdb?Title, mdb?season_number, omdb?imdbRating))
  |> Seq.where (fun (date, title, season, rating) -> not (rating.AsString() = "N/A") && not (season.AsString() = "N/A"))
  |> Seq.map (fun (date, title, season, rating) -> (date.AsDateTime(), title.AsString(), season.AsInteger(), rating.AsDecimal()))
  |> Seq.groupBy (fun (date, title, season, rating) -> season)
  |> Seq.map (fun (season, episodes) -> (season, (minRating episodes), (avgRating episodes), (avgRating episodes), (maxRating episodes)))
  |> Seq.sortBy (fun (season, min, max, high, low) -> season)
  //|> Seq.iter (fun r -> printfn "%A" r)

Chart.Candlestick(ratingsBySeason).Show()

episodeSources
|> Seq.map (fun es -> loadJson es.Id)
|> Seq.map (fun (omdb, wms) -> (wms?data?released, omdb?imdbRating))
|> Seq.where (fun (date, rating) -> not (rating.AsString() = "N/A"))
|> Seq.sortBy (fun (date, rating) -> date)

