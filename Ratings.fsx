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

let location = @"E:\Source\Repositories\RedDwarfAnalysis\";

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

let writeFile path id (json : string) extension =
  let fileName = location + path + @"\" + id + extension
  use streamWriter = new StreamWriter(fileName, false)
  streamWriter.WriteLine(json)

let writeJson path id (json : string) =
  writeFile path id json ".json"

let writeHtml path id (json : string) =
  writeFile path id json ".html"

// Using: http://www.omdbapi.com/
episodeSources
|> Seq.map (fun es -> (es.Id,  Http.RequestString ( "http://www.omdbapi.com/", query=["i", es.Id; "tomatoes", "true"] )))
|> Seq.iter (fun (id, json) -> writeJson "Omdb" id json)

// Using: http://imdb.wemakesites.net/
episodeSources
|> Seq.map (fun es -> (es.Id,  Http.RequestString ( "http://imdb.wemakesites.net/api/" + es.Id, query=["api_key", "5ff83107-8872-4324-902a-283fc7626a38"] )))
|> Seq.iter (fun (id, json) -> writeJson "WeMakeSites" id json)

// Using: http://api.themoviedb.org/
episodeSources
|> Seq.map (fun es -> System.Threading.Thread.Sleep(1000); es)
|> Seq.map (fun es -> (es.Id, Http.RequestString ("https://api.themoviedb.org/3/tv/326/season/" + es.Season.ToString() + "/episode/" + es.Episode.ToString(), query=["api_key", "1b9b9807adc4f6af36df49c81b20f0fc"])))
|> Seq.iter (fun (id, json) -> writeJson "TheMovieDb" id json)

// Using IMDB ratings
episodeSources
|> Seq.map (fun es -> System.Threading.Thread.Sleep(1000); es)
|> Seq.map (fun es -> (es.Id, Http.RequestString ("http://www.imdb.com/title/" + es.Id + "/ratings")))
|> Seq.iter (fun (id, json) -> writeHtml "Ratings" id json)

type OmdbRecord = JsonProvider< @"C:\Source\Repositories\Spikes\RedDwarfAnalysis\Omdb\tt0684181.json" >
type WeMakeSitesRecord = JsonProvider< @"C:\Source\Repositories\Spikes\RedDwarfAnalysis\WeMakeSites\tt0684181.json" >
type TheMoviewDbRecord = JsonProvider< @"C:\Source\Repositories\Spikes\RedDwarfAnalysis\TheMovieDb\tt0684181.json" >

type RatingsType = HtmlProvider< @"C:\Source\Repositories\Spikes\RedDwarfAnalysis\Ratings\tt0684182.htm" >
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

let tryFind (dict : System.Collections.Generic.IDictionary<'a,'b>) (key : 'a) =
  let containsKey = dict.ContainsKey(key)
  match containsKey with
  | true -> Some dict.[key]
  | false -> None

let pivotRatings (ratings : EpisodeRatings seq) =
   let dictionary = 
     ratings
     |> Seq.map (fun r -> (r.Category, r.Rating))
     |> dict
   let rating = {
     ``Males`` = (tryFind dictionary RatingCategory.``Males``);
     ``Females`` = (tryFind dictionary RatingCategory.``Females``);
     ``Aged under 18`` = (tryFind dictionary RatingCategory.``Aged under 18``);
     ``Males under 18`` = (tryFind dictionary RatingCategory.``Males under 18``);
     ``Aged 18-29`` = (tryFind dictionary RatingCategory.``Aged 18-29``);
     ``Males Aged 18-29`` = (tryFind dictionary RatingCategory.``Males Aged 18-29``);
     ``Females Aged 18-29`` = (tryFind dictionary RatingCategory.``Females Aged 18-29``);
     ``Aged 30-44`` = (tryFind dictionary RatingCategory.``Aged 30-44``);
     ``Males Aged 30-44`` = (tryFind dictionary RatingCategory.``Males Aged 30-44``);
     ``Females Aged 30-44`` = (tryFind dictionary RatingCategory.``Females Aged 30-44``);
     ``Aged 45`` = (tryFind dictionary RatingCategory.``Aged 45``);
     ``Males Aged 45`` = (tryFind dictionary RatingCategory.``Males Aged 45``);
     ``Females Aged 45`` = (tryFind dictionary RatingCategory.``Females Aged 45``);
     ``Top 1000 voters`` = (tryFind dictionary RatingCategory.``Top 1000 voters``);
     ``US users`` = (tryFind dictionary RatingCategory.``US users``);
     ``Non-US users`` = (tryFind dictionary RatingCategory.``Non-US users``)
   }
   rating
     
let loadOmdbFile id =
  let fileName = location + @"Omdb\" + id + ".json"
  OmdbRecord.Load(fileName) 
  
let loadWeMakeSitesFile id =
  let fileName = location + @"WeMakeSites\" + id + ".json"
  WeMakeSitesRecord.Load(fileName) 
  
let loadTheMovieDbFile id =
  let fileName = location + @"TheMovieDb\" + id + ".json"
  WeMakeSitesRecord.Load(fileName) 

let loadFiles id =
  let omdbFile = loadOmdbFile id
  let wmsFile = loadWeMakeSitesFile id
  (omdbFile, wmsFile)

let loadOmdbJson id =
  let fileName = location + @"Omdb\" + id + ".json"
  JsonValue.Load(fileName) 
  
let loadWeMakeSitesJson id =
  let fileName = location + @"WeMakeSites\" + id + ".json"
  JsonValue.Load(fileName) 
  
let loadTheMovieDbJson id =
  let fileName = location + @"TheMovieDb\" + id + ".json"
  JsonValue.Load(fileName) 
  
let loadRatings id =
  let ratings = parseRatings id
  let rating = pivotRatings ratings
  rating

let loadJson id =
  let mdbFile = loadTheMovieDbJson id
  let ratings = loadRatings id
  (mdbFile, ratings)
 
let ratingsByDateAndAgeCategory = 
  episodeSources
  |> Seq.map (fun es -> loadJson es.Id)
  |> Seq.map (fun (mdb, ratings) -> (mdb?air_date.AsDateTime(), ratings))
  |> Seq.collect (fun (date, ratings) -> [| (date, RatingCategory.``Aged under 18``, ratings.``Aged under 18``); (date, RatingCategory.``Aged 18-29``, ratings.``Aged 18-29``); (date, RatingCategory.``Aged 30-44``, ratings.``Aged 30-44``); (date, RatingCategory.``Aged 45``, ratings.``Aged 45``)|])
  |> Seq.where (fun (date, category, rating) -> rating.IsSome)
  |> Seq.map (fun (date, category, rating) -> (date, category, rating.Value))
  |> Seq.groupBy (fun (date, category, rating) -> category)
  |> Seq.map (fun (key, values) -> values |> Seq.map (fun (date, category, rating) -> (date, rating)) |> Seq.sortBy (fun (date, rating) -> date))


let ratingsByDate = 
  episodeSources
  |> Seq.map (fun es -> loadJson es.Id)
  |> Seq.map (fun (omdb, wms, mdb, ratings) -> (wms?data?released, omdb?Title, ratings.``Top 1000 voters``))
  |> Seq.where (fun (date, title, rating) -> rating.IsSome)
  |> Seq.map (fun (date, title, rating) -> (date.AsDateTime(), title.AsString(), rating.Value))
  |> Seq.sortBy (fun (date, title, rating) -> date)

// Generates scatter chart with trend line
let values = 
  ratingsByDate
  |> Seq.map (fun (date, title, rating) -> (date, rating))

let options = Options(pointSize=3, colors=[|"#3B8FCC"|], trendlines=[|Trendline(opacity=0.5,lineWidth=5,color="#C0D9EA")|], hAxis=Axis(title="Date"), vAxis=Axis(title="Rating"))
Chart.Scatter(values) |> Chart.WithOptions(options) |> Chart.Show

let ratingsByDateAndAgeCategory = 
  episodeSources
  |> Seq.map (fun es -> loadJson es.Id)
  |> Seq.map (fun (mdb, ratings) -> (mdb?air_date.AsDateTime(), ratings))
  |> Seq.collect (fun (date, ratings) -> [| (date, RatingCategory.``Aged under 18``, ratings.``Aged under 18``); (date, RatingCategory.``Aged 18-29``, ratings.``Aged 18-29``); (date, RatingCategory.``Aged 30-44``, ratings.``Aged 30-44``); (date, RatingCategory.``Aged 45``, ratings.``Aged 45``)|])
  |> Seq.where (fun (date, category, rating) -> rating.IsSome)
  |> Seq.map (fun (date, category, rating) -> (date, category, rating.Value))
  |> Seq.groupBy (fun (date, category, rating) -> category)
  |> Seq.map (fun (key, values) -> values |> Seq.map (fun (date, category, rating) -> (date, rating)) |> Seq.sortBy (fun (date, rating) -> date))
  |> Seq.toList

let options = 
  Options(
    pointSize=3, 
    colors=[|"#6AA590"; "#7DE6C1"; "#57E6B3"; "#60A6D0"; "#3B8FCC"|], 
    trendlines=[|
      Trendline(opacity=0.5,lineWidth=5,color="#6AA590");
      Trendline(opacity=0.5,lineWidth=5,color="#7DE6C1");
      Trendline(opacity=0.5,lineWidth=5,color="#57E6B3");
      Trendline(opacity=0.5,lineWidth=5,color="#60A6D0");
      Trendline(opacity=0.5,lineWidth=5,color="#3B8FCC")|],      
    hAxis=Axis(title="Date"),
    vAxis=Axis(title="Rating"))

Chart.Scatter(ratingsByDateAndAgeCategory, [|"Aged under 18"; "Aged 18-29";"Aged 30-44";"Aged 45+";"????"|])
|> Chart.WithOptions(options) 
|> Chart.WithLegend(true)
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



let ratings = parseRatings "tt1997038"
let rating = loadRatings "tt0684181"

episodeSources
|> Seq.collect (fun es -> parseRatings es.Id)
|> Seq.groupBy (fun er -> er.Id)
//|> Seq.where (fun (key, values) -> (Seq.length values) < 16)
|> Seq.iter (fun (key, values) -> printfn "Id %s has %i values" key (Seq.length values))

episodeSources
|> Seq.map (fun es -> loadRatings es.Id)
|> Seq.iter (fun r -> printfn "%A" r)