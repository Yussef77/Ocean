namespace BlazorAppShared.Services {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlazorAppShared.Model;

    public class AddressService {
        IList<CityStateZip> _items;

        public AddressService() {
        }

        public async Task<IEnumerable<CityStateZip>> SearchByCityAsync(String citySearchText) {
            if (_items == null) {
                LoadItems();
            }
            var searchText = citySearchText.ToLower();
            var results = await Task.FromResult<IEnumerable<CityStateZip>>(_items.Where(x => x.SearchKey.StartsWith(searchText)).ToList());
            return results;
        }

        public async Task<IEnumerable<CityStateZip>> SearchByZipAsync(String zipSearchText) {
            if (_items == null) {
                LoadItems();
            }
            var results = await Task.FromResult<IEnumerable<CityStateZip>>(_items.Where(x => x.Zip.StartsWith(zipSearchText)).ToList());
            return results;
        }

        void LoadItems() {
            const String json = @"[
              {
                zip: ""46910"",
                city: ""Akron"",
                state: ""IN""
              },
              {
                zip: ""47916"",
                city: ""Alamo"",
                state: ""IN""
              },
              {
                zip: ""47320"",
                city: ""Albany"",
                state: ""IN""
              },
              {
                zip: ""46701"",
                city: ""Albion"",
                state: ""IN""
              },
              {
                zip: ""46001"",
                city: ""Alexandria"",
                state: ""IN""
              },
              {
                zip: ""47917"",
                city: ""Ambia"",
                state: ""IN""
              },
              {
                zip: ""46911"",
                city: ""Amboy"",
                state: ""IN""
              },
              {
                zip: ""46103"",
                city: ""Amo"",
                state: ""IN""
              },
              {
                zip: ""46011"",
                city: ""Anderson"",
                state: ""IN""
              },
              {
                zip: ""46012"",
                city: ""Anderson"",
                state: ""IN""
              },
              {
                zip: ""46013"",
                city: ""Anderson"",
                state: ""IN""
              },
              {
                zip: ""46016"",
                city: ""Anderson"",
                state: ""IN""
              },
              {
                zip: ""46017"",
                city: ""Anderson"",
                state: ""IN""
              },
              {
                zip: ""46702"",
                city: ""Andrews"",
                state: ""IN""
              },
              {
                zip: ""46703"",
                city: ""Angola"",
                state: ""IN""
              },
              {
                zip: ""46030"",
                city: ""Arcadia"",
                state: ""IN""
              },
              {
                zip: ""46704"",
                city: ""Arcola"",
                state: ""IN""
              },
              {
                zip: ""46501"",
                city: ""Argos"",
                state: ""IN""
              },
              {
                zip: ""46104"",
                city: ""Arlington"",
                state: ""IN""
              },
              {
                zip: ""46705"",
                city: ""Ashley"",
                state: ""IN""
              },
              {
                zip: ""46031"",
                city: ""Atlanta"",
                state: ""IN""
              },
              {
                zip: ""47918"",
                city: ""Attica"",
                state: ""IN""
              },
              {
                zip: ""46502"",
                city: ""Atwood"",
                state: ""IN""
              },
              {
                zip: ""46706"",
                city: ""Auburn"",
                state: ""IN""
              },
              {
                zip: ""47001"",
                city: ""Aurora"",
                state: ""IN""
              },
              {
                zip: ""47102"",
                city: ""Austin"",
                state: ""IN""
              },
              {
                zip: ""46710"",
                city: ""Avilla"",
                state: ""IN""
              },
              {
                zip: ""47420"",
                city: ""Avoca"",
                state: ""IN""
              },
              {
                zip: ""46123"",
                city: ""Avon"",
                state: ""IN""
              },
              {
                zip: ""46105"",
                city: ""Bainbridge"",
                state: ""IN""
              },
              {
                zip: ""46106"",
                city: ""Bargersville"",
                state: ""IN""
              },
              {
                zip: ""47006"",
                city: ""Batesville"",
                state: ""IN""
              },
              {
                zip: ""47010"",
                city: ""Bath"",
                state: ""IN""
              },
              {
                zip: ""47920"",
                city: ""Battle Ground"",
                state: ""IN""
              },
              {
                zip: ""47421"",
                city: ""Bedford"",
                state: ""IN""
              },
              {
                zip: ""46107"",
                city: ""Beech Grove"",
                state: ""IN""
              },
              {
                zip: ""47011"",
                city: ""Bennington"",
                state: ""IN""
              },
              {
                zip: ""46711"",
                city: ""Berne"",
                state: ""IN""
              },
              {
                zip: ""47104"",
                city: ""Bethlehem"",
                state: ""IN""
              },
              {
                zip: ""46301"",
                city: ""Beverly Shores"",
                state: ""IN""
              },
              {
                zip: ""47512"",
                city: ""Bicknell"",
                state: ""IN""
              },
              {
                zip: ""47513"",
                city: ""Birdseye"",
                state: ""IN""
              },
              {
                zip: ""47424"",
                city: ""Bloomfield"",
                state: ""IN""
              },
              {
                zip: ""47832"",
                city: ""Bloomingdale"",
                state: ""IN""
              },
              {
                zip: ""47401"",
                city: ""Bloomington"",
                state: ""IN""
              },
              {
                zip: ""47403"",
                city: ""Bloomington"",
                state: ""IN""
              },
              {
                zip: ""47404"",
                city: ""Bloomington"",
                state: ""IN""
              },
              {
                zip: ""47405"",
                city: ""Bloomington"",
                state: ""IN""
              },
              {
                zip: ""47406"",
                city: ""Bloomington"",
                state: ""IN""
              },
              {
                zip: ""47408"",
                city: ""Bloomington"",
                state: ""IN""
              },
              {
                zip: ""46714"",
                city: ""Bluffton"",
                state: ""IN""
              },
              {
                zip: ""46110"",
                city: ""Boggstown"",
                state: ""IN""
              },
              {
                zip: ""47601"",
                city: ""Boonville"",
                state: ""IN""
              },
              {
                zip: ""47106"",
                city: ""Borden"",
                state: ""IN""
              },
              {
                zip: ""47324"",
                city: ""Boston"",
                state: ""IN""
              },
              {
                zip: ""47921"",
                city: ""Boswell"",
                state: ""IN""
              },
              {
                zip: ""46504"",
                city: ""Bourbon"",
                state: ""IN""
              },
              {
                zip: ""47833"",
                city: ""Bowling Green"",
                state: ""IN""
              },
              {
                zip: ""47514"",
                city: ""Branchville"",
                state: ""IN""
              },
              {
                zip: ""47834"",
                city: ""Brazil"",
                state: ""IN""
              },
              {
                zip: ""46506"",
                city: ""Bremen"",
                state: ""IN""
              },
              {
                zip: ""47836"",
                city: ""Bridgeton"",
                state: ""IN""
              },
              {
                zip: ""46913"",
                city: ""Bringhurst"",
                state: ""IN""
              },
              {
                zip: ""46507"",
                city: ""Bristol"",
                state: ""IN""
              },
              {
                zip: ""47515"",
                city: ""Bristow"",
                state: ""IN""
              },
              {
                zip: ""47922"",
                city: ""Brook"",
                state: ""IN""
              },
              {
                zip: ""46111"",
                city: ""Brooklyn"",
                state: ""IN""
              },
              {
                zip: ""47923"",
                city: ""Brookston"",
                state: ""IN""
              },
              {
                zip: ""47012"",
                city: ""Brookville"",
                state: ""IN""
              },
              {
                zip: ""46112"",
                city: ""Brownsburg"",
                state: ""IN""
              },
              {
                zip: ""47220"",
                city: ""Brownstown"",
                state: ""IN""
              },
              {
                zip: ""47325"",
                city: ""Brownsville"",
                state: ""IN""
              },
              {
                zip: ""47516"",
                city: ""Bruceville"",
                state: ""IN""
              },
              {
                zip: ""47326"",
                city: ""Bryant"",
                state: ""IN""
              },
              {
                zip: ""47924"",
                city: ""Buck Creek"",
                state: ""IN""
              },
              {
                zip: ""47925"",
                city: ""Buffalo"",
                state: ""IN""
              },
              {
                zip: ""46914"",
                city: ""Bunker Hill"",
                state: ""IN""
              },
              {
                zip: ""46508"",
                city: ""Burket"",
                state: ""IN""
              },
              {
                zip: ""46915"",
                city: ""Burlington"",
                state: ""IN""
              },
              {
                zip: ""47926"",
                city: ""Burnettsville"",
                state: ""IN""
              },
              {
                zip: ""46721"",
                city: ""Butler"",
                state: ""IN""
              },
              {
                zip: ""47223"",
                city: ""Butlerville"",
                state: ""IN""
              },
              {
                zip: ""47327"",
                city: ""Cambridge City"",
                state: ""IN""
              },
              {
                zip: ""46113"",
                city: ""Camby"",
                state: ""IN""
              },
              {
                zip: ""46917"",
                city: ""Camden"",
                state: ""IN""
              },
              {
                zip: ""47108"",
                city: ""Campbellsburg"",
                state: ""IN""
              },
              {
                zip: ""47224"",
                city: ""Canaan"",
                state: ""IN""
              },
              {
                zip: ""47519"",
                city: ""Cannelburg"",
                state: ""IN""
              },
              {
                zip: ""47520"",
                city: ""Cannelton"",
                state: ""IN""
              },
              {
                zip: ""47837"",
                city: ""Carbon"",
                state: ""IN""
              },
              {
                zip: ""47838"",
                city: ""Carlisle"",
                state: ""IN""
              },
              {
                zip: ""46032"",
                city: ""Carmel"",
                state: ""IN""
              },
              {
                zip: ""46033"",
                city: ""Carmel"",
                state: ""IN""
              },
              {
                zip: ""46115"",
                city: ""Carthage"",
                state: ""IN""
              },
              {
                zip: ""47928"",
                city: ""Cayuga"",
                state: ""IN""
              },
              {
                zip: ""47016"",
                city: ""Cedar Grove"",
                state: ""IN""
              },
              {
                zip: ""46303"",
                city: ""Cedar Lake"",
                state: ""IN""
              },
              {
                zip: ""47521"",
                city: ""Celestine"",
                state: ""IN""
              },
              {
                zip: ""47840"",
                city: ""Centerpoint"",
                state: ""IN""
              },
              {
                zip: ""47330"",
                city: ""Centerville"",
                state: ""IN""
              },
              {
                zip: ""47110"",
                city: ""Central"",
                state: ""IN""
              },
              {
                zip: ""47929"",
                city: ""Chalmers"",
                state: ""IN""
              },
              {
                zip: ""47610"",
                city: ""Chandler"",
                state: ""IN""
              },
              {
                zip: ""47111"",
                city: ""Charlestown"",
                state: ""IN""
              },
              {
                zip: ""46117"",
                city: ""Charlottesville"",
                state: ""IN""
              },
              {
                zip: ""46304"",
                city: ""Chesterton"",
                state: ""IN""
              },
              {
                zip: ""47611"",
                city: ""Chrisney"",
                state: ""IN""
              },
              {
                zip: ""46723"",
                city: ""Churubusco"",
                state: ""IN""
              },
              {
                zip: ""46034"",
                city: ""Cicero"",
                state: ""IN""
              },
              {
                zip: ""47930"",
                city: ""Clarks Hill"",
                state: ""IN""
              },
              {
                zip: ""47129"",
                city: ""Clarksville"",
                state: ""IN""
              },
              {
                zip: ""47841"",
                city: ""Clay City"",
                state: ""IN""
              },
              {
                zip: ""46510"",
                city: ""Claypool"",
                state: ""IN""
              },
              {
                zip: ""46118"",
                city: ""Clayton"",
                state: ""IN""
              },
              {
                zip: ""47226"",
                city: ""Clifford"",
                state: ""IN""
              },
              {
                zip: ""47842"",
                city: ""Clinton"",
                state: ""IN""
              },
              {
                zip: ""46120"",
                city: ""Cloverdale"",
                state: ""IN""
              },
              {
                zip: ""47427"",
                city: ""Coal City"",
                state: ""IN""
              },
              {
                zip: ""46121"",
                city: ""Coatesville"",
                state: ""IN""
              },
              {
                zip: ""46035"",
                city: ""Colfax"",
                state: ""IN""
              },
              {
                zip: ""46725"",
                city: ""Columbia City"",
                state: ""IN""
              },
              {
                zip: ""47201"",
                city: ""Columbus"",
                state: ""IN""
              },
              {
                zip: ""47203"",
                city: ""Columbus"",
                state: ""IN""
              },
              {
                zip: ""47227"",
                city: ""Commiskey"",
                state: ""IN""
              },
              {
                zip: ""47331"",
                city: ""Connersville"",
                state: ""IN""
              },
              {
                zip: ""46919"",
                city: ""Converse"",
                state: ""IN""
              },
              {
                zip: ""46730"",
                city: ""Corunna"",
                state: ""IN""
              },
              {
                zip: ""47846"",
                city: ""Cory"",
                state: ""IN""
              },
              {
                zip: ""47112"",
                city: ""Corydon"",
                state: ""IN""
              },
              {
                zip: ""47932"",
                city: ""Covington"",
                state: ""IN""
              },
              {
                zip: ""46731"",
                city: ""Craigville"",
                state: ""IN""
              },
              {
                zip: ""47114"",
                city: ""Crandall"",
                state: ""IN""
              },
              {
                zip: ""47522"",
                city: ""Crane"",
                state: ""IN""
              },
              {
                zip: ""47933"",
                city: ""Crawfordsville"",
                state: ""IN""
              },
              {
                zip: ""46732"",
                city: ""Cromwell"",
                state: ""IN""
              },
              {
                zip: ""47017"",
                city: ""Cross Plains"",
                state: ""IN""
              },
              {
                zip: ""47229"",
                city: ""Crothersville"",
                state: ""IN""
              },
              {
                zip: ""46307"",
                city: ""Crown Point"",
                state: ""IN""
              },
              {
                zip: ""46511"",
                city: ""Culver"",
                state: ""IN""
              },
              {
                zip: ""46920"",
                city: ""Cutler"",
                state: ""IN""
              },
              {
                zip: ""47612"",
                city: ""Cynthiana"",
                state: ""IN""
              },
              {
                zip: ""47523"",
                city: ""Dale"",
                state: ""IN""
              },
              {
                zip: ""47334"",
                city: ""Daleville"",
                state: ""IN""
              },
              {
                zip: ""47847"",
                city: ""Dana"",
                state: ""IN""
              },
              {
                zip: ""46122"",
                city: ""Danville"",
                state: ""IN""
              },
              {
                zip: ""47940"",
                city: ""Darlington"",
                state: ""IN""
              },
              {
                zip: ""47941"",
                city: ""Dayton"",
                state: ""IN""
              },
              {
                zip: ""46733"",
                city: ""Decatur"",
                state: ""IN""
              },
              {
                zip: ""47524"",
                city: ""Decker"",
                state: ""IN""
              },
              {
                zip: ""46922"",
                city: ""Delong"",
                state: ""IN""
              },
              {
                zip: ""46923"",
                city: ""Delphi"",
                state: ""IN""
              },
              {
                zip: ""46310"",
                city: ""Demotte"",
                state: ""IN""
              },
              {
                zip: ""46926"",
                city: ""Denver"",
                state: ""IN""
              },
              {
                zip: ""47115"",
                city: ""Depauw"",
                state: ""IN""
              },
              {
                zip: ""47230"",
                city: ""Deputy"",
                state: ""IN""
              },
              {
                zip: ""47525"",
                city: ""Derby"",
                state: ""IN""
              },
              {
                zip: ""47018"",
                city: ""Dillsboro"",
                state: ""IN""
              },
              {
                zip: ""47335"",
                city: ""Dublin"",
                state: ""IN""
              },
              {
                zip: ""47527"",
                city: ""Dubois"",
                state: ""IN""
              },
              {
                zip: ""47848"",
                city: ""Dugger"",
                state: ""IN""
              },
              {
                zip: ""47336"",
                city: ""Dunkirk"",
                state: ""IN""
              },
              {
                zip: ""47337"",
                city: ""Dunreith"",
                state: ""IN""
              },
              {
                zip: ""47231"",
                city: ""Dupont"",
                state: ""IN""
              },
              {
                zip: ""46311"",
                city: ""Dyer"",
                state: ""IN""
              },
              {
                zip: ""47942"",
                city: ""Earl Park"",
                state: ""IN""
              },
              {
                zip: ""46312"",
                city: ""East Chicago"",
                state: ""IN""
              },
              {
                zip: ""47338"",
                city: ""Eaton"",
                state: ""IN""
              },
              {
                zip: ""47116"",
                city: ""Eckerty"",
                state: ""IN""
              },
              {
                zip: ""47339"",
                city: ""Economy"",
                state: ""IN""
              },
              {
                zip: ""46124"",
                city: ""Edinburgh"",
                state: ""IN""
              },
              {
                zip: ""47528"",
                city: ""Edwardsport"",
                state: ""IN""
              },
              {
                zip: ""47613"",
                city: ""Elberfeld"",
                state: ""IN""
              },
              {
                zip: ""47117"",
                city: ""Elizabeth"",
                state: ""IN""
              },
              {
                zip: ""47232"",
                city: ""Elizabethtown"",
                state: ""IN""
              },
              {
                zip: ""46514"",
                city: ""Elkhart"",
                state: ""IN""
              },
              {
                zip: ""46516"",
                city: ""Elkhart"",
                state: ""IN""
              },
              {
                zip: ""46517"",
                city: ""Elkhart"",
                state: ""IN""
              },
              {
                zip: ""47429"",
                city: ""Ellettsville"",
                state: ""IN""
              },
              {
                zip: ""47529"",
                city: ""Elnora"",
                state: ""IN""
              },
              {
                zip: ""46036"",
                city: ""Elwood"",
                state: ""IN""
              },
              {
                zip: ""46125"",
                city: ""Eminence"",
                state: ""IN""
              },
              {
                zip: ""47118"",
                city: ""English"",
                state: ""IN""
              },
              {
                zip: ""46524"",
                city: ""Etna Green"",
                state: ""IN""
              },
              {
                zip: ""47531"",
                city: ""Evanston"",
                state: ""IN""
              },
              {
                zip: ""47708"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47710"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47711"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47712"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47713"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47714"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47715"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47720"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47725"",
                city: ""Evansville"",
                state: ""IN""
              },
              {
                zip: ""47943"",
                city: ""Fair Oaks"",
                state: ""IN""
              },
              {
                zip: ""47849"",
                city: ""Fairbanks"",
                state: ""IN""
              },
              {
                zip: ""46126"",
                city: ""Fairland"",
                state: ""IN""
              },
              {
                zip: ""46928"",
                city: ""Fairmount"",
                state: ""IN""
              },
              {
                zip: ""46127"",
                city: ""Falmouth"",
                state: ""IN""
              },
              {
                zip: ""47850"",
                city: ""Farmersburg"",
                state: ""IN""
              },
              {
                zip: ""47340"",
                city: ""Farmland"",
                state: ""IN""
              },
              {
                zip: ""47532"",
                city: ""Ferdinand"",
                state: ""IN""
              },
              {
                zip: ""46128"",
                city: ""Fillmore"",
                state: ""IN""
              },
              {
                zip: ""46037"",
                city: ""Fishers"",
                state: ""IN""
              },
              {
                zip: ""46038"",
                city: ""Fishers"",
                state: ""IN""
              },
              {
                zip: ""47234"",
                city: ""Flat Rock"",
                state: ""IN""
              },
              {
                zip: ""46929"",
                city: ""Flora"",
                state: ""IN""
              },
              {
                zip: ""47020"",
                city: ""Florence"",
                state: ""IN""
              },
              {
                zip: ""47119"",
                city: ""Floyds Knobs"",
                state: ""IN""
              },
              {
                zip: ""46039"",
                city: ""Forest"",
                state: ""IN""
              },
              {
                zip: ""47648"",
                city: ""Fort Branch"",
                state: ""IN""
              },
              {
                zip: ""46802"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46803"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46804"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46805"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46806"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46807"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46808"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46809"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46814"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46815"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46816"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46818"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46819"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46825"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46835"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46845"",
                city: ""Fort Wayne"",
                state: ""IN""
              },
              {
                zip: ""46040"",
                city: ""Fortville"",
                state: ""IN""
              },
              {
                zip: ""47341"",
                city: ""Fountain City"",
                state: ""IN""
              },
              {
                zip: ""46130"",
                city: ""Fountaintown"",
                state: ""IN""
              },
              {
                zip: ""47944"",
                city: ""Fowler"",
                state: ""IN""
              },
              {
                zip: ""46930"",
                city: ""Fowlerton"",
                state: ""IN""
              },
              {
                zip: ""47946"",
                city: ""Francesville"",
                state: ""IN""
              },
              {
                zip: ""47649"",
                city: ""Francisco"",
                state: ""IN""
              },
              {
                zip: ""46041"",
                city: ""Frankfort"",
                state: ""IN""
              },
              {
                zip: ""46131"",
                city: ""Franklin"",
                state: ""IN""
              },
              {
                zip: ""46044"",
                city: ""Frankton"",
                state: ""IN""
              },
              {
                zip: ""47120"",
                city: ""Fredericksburg"",
                state: ""IN""
              },
              {
                zip: ""47431"",
                city: ""Freedom"",
                state: ""IN""
              },
              {
                zip: ""47535"",
                city: ""Freelandville"",
                state: ""IN""
              },
              {
                zip: ""47235"",
                city: ""Freetown"",
                state: ""IN""
              },
              {
                zip: ""46737"",
                city: ""Fremont"",
                state: ""IN""
              },
              {
                zip: ""47432"",
                city: ""French Lick"",
                state: ""IN""
              },
              {
                zip: ""47536"",
                city: ""Fulda"",
                state: ""IN""
              },
              {
                zip: ""46931"",
                city: ""Fulton"",
                state: ""IN""
              },
              {
                zip: ""46932"",
                city: ""Galveston"",
                state: ""IN""
              },
              {
                zip: ""46738"",
                city: ""Garrett"",
                state: ""IN""
              },
              {
                zip: ""46402"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46403"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46404"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46406"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46407"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46408"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46409"",
                city: ""Gary"",
                state: ""IN""
              },
              {
                zip: ""46933"",
                city: ""Gas City"",
                state: ""IN""
              },
              {
                zip: ""47342"",
                city: ""Gaston"",
                state: ""IN""
              },
              {
                zip: ""46740"",
                city: ""Geneva"",
                state: ""IN""
              },
              {
                zip: ""47537"",
                city: ""Gentryville"",
                state: ""IN""
              },
              {
                zip: ""47122"",
                city: ""Georgetown"",
                state: ""IN""
              },
              {
                zip: ""46133"",
                city: ""Glenwood"",
                state: ""IN""
              },
              {
                zip: ""46045"",
                city: ""Goldsmith"",
                state: ""IN""
              },
              {
                zip: ""47948"",
                city: ""Goodland"",
                state: ""IN""
              },
              {
                zip: ""46526"",
                city: ""Goshen"",
                state: ""IN""
              },
              {
                zip: ""46528"",
                city: ""Goshen"",
                state: ""IN""
              },
              {
                zip: ""47433"",
                city: ""Gosport"",
                state: ""IN""
              },
              {
                zip: ""46741"",
                city: ""Grabill"",
                state: ""IN""
              },
              {
                zip: ""47615"",
                city: ""Grandview"",
                state: ""IN""
              },
              {
                zip: ""46530"",
                city: ""Granger"",
                state: ""IN""
              },
              {
                zip: ""47123"",
                city: ""Grantsburg"",
                state: ""IN""
              },
              {
                zip: ""46135"",
                city: ""Greencastle"",
                state: ""IN""
              },
              {
                zip: ""46140"",
                city: ""Greenfield"",
                state: ""IN""
              },
              {
                zip: ""47345"",
                city: ""Greens Fork"",
                state: ""IN""
              },
              {
                zip: ""47344"",
                city: ""Greensboro"",
                state: ""IN""
              },
              {
                zip: ""47240"",
                city: ""Greensburg"",
                state: ""IN""
              },
              {
                zip: ""46936"",
                city: ""Greentown"",
                state: ""IN""
              },
              {
                zip: ""47124"",
                city: ""Greenville"",
                state: ""IN""
              },
              {
                zip: ""46142"",
                city: ""Greenwood"",
                state: ""IN""
              },
              {
                zip: ""46143"",
                city: ""Greenwood"",
                state: ""IN""
              },
              {
                zip: ""47616"",
                city: ""Griffin"",
                state: ""IN""
              },
              {
                zip: ""46319"",
                city: ""Griffith"",
                state: ""IN""
              },
              {
                zip: ""46531"",
                city: ""Grovertown"",
                state: ""IN""
              },
              {
                zip: ""47022"",
                city: ""Guilford"",
                state: ""IN""
              },
              {
                zip: ""46144"",
                city: ""Gwynneville"",
                state: ""IN""
              },
              {
                zip: ""47346"",
                city: ""Hagerstown"",
                state: ""IN""
              },
              {
                zip: ""46742"",
                city: ""Hamilton"",
                state: ""IN""
              },
              {
                zip: ""46532"",
                city: ""Hamlet"",
                state: ""IN""
              },
              {
                zip: ""46320"",
                city: ""Hammond"",
                state: ""IN""
              },
              {
                zip: ""46323"",
                city: ""Hammond"",
                state: ""IN""
              },
              {
                zip: ""46324"",
                city: ""Hammond"",
                state: ""IN""
              },
              {
                zip: ""46327"",
                city: ""Hammond"",
                state: ""IN""
              },
              {
                zip: ""46340"",
                city: ""Hanna"",
                state: ""IN""
              },
              {
                zip: ""47243"",
                city: ""Hanover"",
                state: ""IN""
              },
              {
                zip: ""47125"",
                city: ""Hardinsburg"",
                state: ""IN""
              },
              {
                zip: ""46743"",
                city: ""Harlan"",
                state: ""IN""
              },
              {
                zip: ""47853"",
                city: ""Harmony"",
                state: ""IN""
              },
              {
                zip: ""47434"",
                city: ""Harrodsburg"",
                state: ""IN""
              },
              {
                zip: ""47348"",
                city: ""Hartford City"",
                state: ""IN""
              },
              {
                zip: ""47244"",
                city: ""Hartsville"",
                state: ""IN""
              },
              {
                zip: ""47639"",
                city: ""Haubstadt"",
                state: ""IN""
              },
              {
                zip: ""47640"",
                city: ""Hazleton"",
                state: ""IN""
              },
              {
                zip: ""46341"",
                city: ""Hebron"",
                state: ""IN""
              },
              {
                zip: ""47436"",
                city: ""Heltonville"",
                state: ""IN""
              },
              {
                zip: ""47126"",
                city: ""Henryville"",
                state: ""IN""
              },
              {
                zip: ""46322"",
                city: ""Highland"",
                state: ""IN""
              },
              {
                zip: ""47949"",
                city: ""Hillsboro"",
                state: ""IN""
              },
              {
                zip: ""47854"",
                city: ""Hillsdale"",
                state: ""IN""
              },
              {
                zip: ""46745"",
                city: ""Hoagland"",
                state: ""IN""
              },
              {
                zip: ""46342"",
                city: ""Hobart"",
                state: ""IN""
              },
              {
                zip: ""46047"",
                city: ""Hobbs"",
                state: ""IN""
              },
              {
                zip: ""47541"",
                city: ""Holland"",
                state: ""IN""
              },
              {
                zip: ""47023"",
                city: ""Holton"",
                state: ""IN""
              },
              {
                zip: ""46146"",
                city: ""Homer"",
                state: ""IN""
              },
              {
                zip: ""47246"",
                city: ""Hope"",
                state: ""IN""
              },
              {
                zip: ""46746"",
                city: ""Howe"",
                state: ""IN""
              },
              {
                zip: ""46747"",
                city: ""Hudson"",
                state: ""IN""
              },
              {
                zip: ""46748"",
                city: ""Huntertown"",
                state: ""IN""
              },
              {
                zip: ""47542"",
                city: ""Huntingburg"",
                state: ""IN""
              },
              {
                zip: ""46750"",
                city: ""Huntington"",
                state: ""IN""
              },
              {
                zip: ""47437"",
                city: ""Huron"",
                state: ""IN""
              },
              {
                zip: ""47855"",
                city: ""Hymera"",
                state: ""IN""
              },
              {
                zip: ""47950"",
                city: ""Idaville"",
                state: ""IN""
              },
              {
                zip: ""46201"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46202"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46203"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46204"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46205"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46208"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46214"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46216"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46217"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46218"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46219"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46220"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46221"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46222"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46224"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46225"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46226"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46227"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46228"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46229"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46231"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46234"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46235"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46236"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46237"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46239"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46240"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46241"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46250"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46254"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46256"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46259"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46260"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46268"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46278"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46280"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46290"",
                city: ""Indianapolis"",
                state: ""IN""
              },
              {
                zip: ""46048"",
                city: ""Ingalls"",
                state: ""IN""
              },
              {
                zip: ""46147"",
                city: ""Jamestown"",
                state: ""IN""
              },
              {
                zip: ""47438"",
                city: ""Jasonville"",
                state: ""IN""
              },
              {
                zip: ""47546"",
                city: ""Jasper"",
                state: ""IN""
              },
              {
                zip: ""47130"",
                city: ""Jeffersonville"",
                state: ""IN""
              },
              {
                zip: ""46938"",
                city: ""Jonesboro"",
                state: ""IN""
              },
              {
                zip: ""47247"",
                city: ""Jonesville"",
                state: ""IN""
              },
              {
                zip: ""46049"",
                city: ""Kempton"",
                state: ""IN""
              },
              {
                zip: ""46755"",
                city: ""Kendallville"",
                state: ""IN""
              },
              {
                zip: ""47351"",
                city: ""Kennard"",
                state: ""IN""
              },
              {
                zip: ""47951"",
                city: ""Kentland"",
                state: ""IN""
              },
              {
                zip: ""46939"",
                city: ""Kewanna"",
                state: ""IN""
              },
              {
                zip: ""46759"",
                city: ""Keystone"",
                state: ""IN""
              },
              {
                zip: ""46760"",
                city: ""Kimmell"",
                state: ""IN""
              },
              {
                zip: ""47952"",
                city: ""Kingman"",
                state: ""IN""
              },
              {
                zip: ""46345"",
                city: ""Kingsbury"",
                state: ""IN""
              },
              {
                zip: ""46346"",
                city: ""Kingsford Heights"",
                state: ""IN""
              },
              {
                zip: ""46050"",
                city: ""Kirklin"",
                state: ""IN""
              },
              {
                zip: ""46148"",
                city: ""Knightstown"",
                state: ""IN""
              },
              {
                zip: ""47857"",
                city: ""Knightsville"",
                state: ""IN""
              },
              {
                zip: ""46534"",
                city: ""Knox"",
                state: ""IN""
              },
              {
                zip: ""46901"",
                city: ""Kokomo"",
                state: ""IN""
              },
              {
                zip: ""46902"",
                city: ""Kokomo"",
                state: ""IN""
              },
              {
                zip: ""46347"",
                city: ""Kouts"",
                state: ""IN""
              },
              {
                zip: ""46348"",
                city: ""La Crosse"",
                state: ""IN""
              },
              {
                zip: ""46940"",
                city: ""La Fontaine"",
                state: ""IN""
              },
              {
                zip: ""46350"",
                city: ""La Porte"",
                state: ""IN""
              },
              {
                zip: ""47135"",
                city: ""Laconia"",
                state: ""IN""
              },
              {
                zip: ""47954"",
                city: ""Ladoga"",
                state: ""IN""
              },
              {
                zip: ""47901"",
                city: ""Lafayette"",
                state: ""IN""
              },
              {
                zip: ""47904"",
                city: ""Lafayette"",
                state: ""IN""
              },
              {
                zip: ""47905"",
                city: ""Lafayette"",
                state: ""IN""
              },
              {
                zip: ""47909"",
                city: ""Lafayette"",
                state: ""IN""
              },
              {
                zip: ""46761"",
                city: ""Lagrange"",
                state: ""IN""
              },
              {
                zip: ""46941"",
                city: ""Lagro"",
                state: ""IN""
              },
              {
                zip: ""46405"",
                city: ""Lake Station"",
                state: ""IN""
              },
              {
                zip: ""46349"",
                city: ""Lake Village"",
                state: ""IN""
              },
              {
                zip: ""46943"",
                city: ""Laketon"",
                state: ""IN""
              },
              {
                zip: ""46536"",
                city: ""Lakeville"",
                state: ""IN""
              },
              {
                zip: ""47550"",
                city: ""Lamar"",
                state: ""IN""
              },
              {
                zip: ""47136"",
                city: ""Lanesville"",
                state: ""IN""
              },
              {
                zip: ""46763"",
                city: ""Laotto"",
                state: ""IN""
              },
              {
                zip: ""46537"",
                city: ""Lapaz"",
                state: ""IN""
              },
              {
                zip: ""46051"",
                city: ""Lapel"",
                state: ""IN""
              },
              {
                zip: ""46764"",
                city: ""Larwill"",
                state: ""IN""
              },
              {
                zip: ""47024"",
                city: ""Laurel"",
                state: ""IN""
              },
              {
                zip: ""47025"",
                city: ""Lawrenceburg"",
                state: ""IN""
              },
              {
                zip: ""47137"",
                city: ""Leavenworth"",
                state: ""IN""
              },
              {
                zip: ""46052"",
                city: ""Lebanon"",
                state: ""IN""
              },
              {
                zip: ""46538"",
                city: ""Leesburg"",
                state: ""IN""
              },
              {
                zip: ""46765"",
                city: ""Leo"",
                state: ""IN""
              },
              {
                zip: ""47551"",
                city: ""Leopold"",
                state: ""IN""
              },
              {
                zip: ""47858"",
                city: ""Lewis"",
                state: ""IN""
              },
              {
                zip: ""47352"",
                city: ""Lewisville"",
                state: ""IN""
              },
              {
                zip: ""47138"",
                city: ""Lexington"",
                state: ""IN""
              },
              {
                zip: ""47353"",
                city: ""Liberty"",
                state: ""IN""
              },
              {
                zip: ""46766"",
                city: ""Liberty Center"",
                state: ""IN""
              },
              {
                zip: ""46946"",
                city: ""Liberty Mills"",
                state: ""IN""
              },
              {
                zip: ""46767"",
                city: ""Ligonier"",
                state: ""IN""
              },
              {
                zip: ""47552"",
                city: ""Lincoln City"",
                state: ""IN""
              },
              {
                zip: ""47955"",
                city: ""Linden"",
                state: ""IN""
              },
              {
                zip: ""47441"",
                city: ""Linton"",
                state: ""IN""
              },
              {
                zip: ""46149"",
                city: ""Lizton"",
                state: ""IN""
              },
              {
                zip: ""46947"",
                city: ""Logansport"",
                state: ""IN""
              },
              {
                zip: ""47553"",
                city: ""Loogootee"",
                state: ""IN""
              },
              {
                zip: ""47354"",
                city: ""Losantville"",
                state: ""IN""
              },
              {
                zip: ""46356"",
                city: ""Lowell"",
                state: ""IN""
              },
              {
                zip: ""46950"",
                city: ""Lucerne"",
                state: ""IN""
              },
              {
                zip: ""47355"",
                city: ""Lynn"",
                state: ""IN""
              },
              {
                zip: ""47619"",
                city: ""Lynnville"",
                state: ""IN""
              },
              {
                zip: ""47443"",
                city: ""Lyons"",
                state: ""IN""
              },
              {
                zip: ""47654"",
                city: ""Mackey"",
                state: ""IN""
              },
              {
                zip: ""46951"",
                city: ""Macy"",
                state: ""IN""
              },
              {
                zip: ""47250"",
                city: ""Madison"",
                state: ""IN""
              },
              {
                zip: ""46150"",
                city: ""Manilla"",
                state: ""IN""
              },
              {
                zip: ""47140"",
                city: ""Marengo"",
                state: ""IN""
              },
              {
                zip: ""46952"",
                city: ""Marion"",
                state: ""IN""
              },
              {
                zip: ""46953"",
                city: ""Marion"",
                state: ""IN""
              },
              {
                zip: ""46770"",
                city: ""Markle"",
                state: ""IN""
              },
              {
                zip: ""46056"",
                city: ""Markleville"",
                state: ""IN""
              },
              {
                zip: ""47859"",
                city: ""Marshall"",
                state: ""IN""
              },
              {
                zip: ""46151"",
                city: ""Martinsville"",
                state: ""IN""
              },
              {
                zip: ""47141"",
                city: ""Marysville"",
                state: ""IN""
              },
              {
                zip: ""46957"",
                city: ""Matthews"",
                state: ""IN""
              },
              {
                zip: ""47142"",
                city: ""Mauckport"",
                state: ""IN""
              },
              {
                zip: ""46155"",
                city: ""Mays"",
                state: ""IN""
              },
              {
                zip: ""46055"",
                city: ""Mccordsville"",
                state: ""IN""
              },
              {
                zip: ""47860"",
                city: ""Mecca"",
                state: ""IN""
              },
              {
                zip: ""47957"",
                city: ""Medaryville"",
                state: ""IN""
              },
              {
                zip: ""47260"",
                city: ""Medora"",
                state: ""IN""
              },
              {
                zip: ""47958"",
                city: ""Mellott"",
                state: ""IN""
              },
              {
                zip: ""47143"",
                city: ""Memphis"",
                state: ""IN""
              },
              {
                zip: ""46539"",
                city: ""Mentone"",
                state: ""IN""
              },
              {
                zip: ""47861"",
                city: ""Merom"",
                state: ""IN""
              },
              {
                zip: ""46410"",
                city: ""Merrillville"",
                state: ""IN""
              },
              {
                zip: ""47030"",
                city: ""Metamora"",
                state: ""IN""
              },
              {
                zip: ""46958"",
                city: ""Mexico"",
                state: ""IN""
              },
              {
                zip: ""46959"",
                city: ""Miami"",
                state: ""IN""
              },
              {
                zip: ""46360"",
                city: ""Michigan City"",
                state: ""IN""
              },
              {
                zip: ""46057"",
                city: ""Michigantown"",
                state: ""IN""
              },
              {
                zip: ""46540"",
                city: ""Middlebury"",
                state: ""IN""
              },
              {
                zip: ""47356"",
                city: ""Middletown"",
                state: ""IN""
              },
              {
                zip: ""47031"",
                city: ""Milan"",
                state: ""IN""
              },
              {
                zip: ""46542"",
                city: ""Milford"",
                state: ""IN""
              },
              {
                zip: ""46365"",
                city: ""Mill Creek"",
                state: ""IN""
              },
              {
                zip: ""46543"",
                city: ""Millersburg"",
                state: ""IN""
              },
              {
                zip: ""47145"",
                city: ""Milltown"",
                state: ""IN""
              },
              {
                zip: ""46156"",
                city: ""Milroy"",
                state: ""IN""
              },
              {
                zip: ""47357"",
                city: ""Milton"",
                state: ""IN""
              },
              {
                zip: ""46544"",
                city: ""Mishawaka"",
                state: ""IN""
              },
              {
                zip: ""46545"",
                city: ""Mishawaka"",
                state: ""IN""
              },
              {
                zip: ""47446"",
                city: ""Mitchell"",
                state: ""IN""
              },
              {
                zip: ""47358"",
                city: ""Modoc"",
                state: ""IN""
              },
              {
                zip: ""46771"",
                city: ""Mongo"",
                state: ""IN""
              },
              {
                zip: ""47959"",
                city: ""Monon"",
                state: ""IN""
              },
              {
                zip: ""46772"",
                city: ""Monroe"",
                state: ""IN""
              },
              {
                zip: ""47557"",
                city: ""Monroe City"",
                state: ""IN""
              },
              {
                zip: ""46773"",
                city: ""Monroeville"",
                state: ""IN""
              },
              {
                zip: ""46157"",
                city: ""Monrovia"",
                state: ""IN""
              },
              {
                zip: ""46960"",
                city: ""Monterey"",
                state: ""IN""
              },
              {
                zip: ""47862"",
                city: ""Montezuma"",
                state: ""IN""
              },
              {
                zip: ""47558"",
                city: ""Montgomery"",
                state: ""IN""
              },
              {
                zip: ""47960"",
                city: ""Monticello"",
                state: ""IN""
              },
              {
                zip: ""47359"",
                city: ""Montpelier"",
                state: ""IN""
              },
              {
                zip: ""47360"",
                city: ""Mooreland"",
                state: ""IN""
              },
              {
                zip: ""47032"",
                city: ""Moores Hill"",
                state: ""IN""
              },
              {
                zip: ""46158"",
                city: ""Mooresville"",
                state: ""IN""
              },
              {
                zip: ""46160"",
                city: ""Morgantown"",
                state: ""IN""
              },
              {
                zip: ""47963"",
                city: ""Morocco"",
                state: ""IN""
              },
              {
                zip: ""46161"",
                city: ""Morristown"",
                state: ""IN""
              },
              {
                zip: ""47964"",
                city: ""Mount Ayr"",
                state: ""IN""
              },
              {
                zip: ""47361"",
                city: ""Mount Summit"",
                state: ""IN""
              },
              {
                zip: ""47620"",
                city: ""Mount Vernon"",
                state: ""IN""
              },
              {
                zip: ""46058"",
                city: ""Mulberry"",
                state: ""IN""
              },
              {
                zip: ""47302"",
                city: ""Muncie"",
                state: ""IN""
              },
              {
                zip: ""47303"",
                city: ""Muncie"",
                state: ""IN""
              },
              {
                zip: ""47304"",
                city: ""Muncie"",
                state: ""IN""
              },
              {
                zip: ""47305"",
                city: ""Muncie"",
                state: ""IN""
              },
              {
                zip: ""47306"",
                city: ""Muncie"",
                state: ""IN""
              },
              {
                zip: ""46321"",
                city: ""Munster"",
                state: ""IN""
              },
              {
                zip: ""47147"",
                city: ""Nabb"",
                state: ""IN""
              },
              {
                zip: ""47034"",
                city: ""Napoleon"",
                state: ""IN""
              },
              {
                zip: ""46550"",
                city: ""Nappanee"",
                state: ""IN""
              },
              {
                zip: ""47448"",
                city: ""Nashville"",
                state: ""IN""
              },
              {
                zip: ""46162"",
                city: ""Needham"",
                state: ""IN""
              },
              {
                zip: ""47150"",
                city: ""New Albany"",
                state: ""IN""
              },
              {
                zip: ""46552"",
                city: ""New Carlisle"",
                state: ""IN""
              },
              {
                zip: ""47362"",
                city: ""New Castle"",
                state: ""IN""
              },
              {
                zip: ""47863"",
                city: ""New Goshen"",
                state: ""IN""
              },
              {
                zip: ""47631"",
                city: ""New Harmony"",
                state: ""IN""
              },
              {
                zip: ""46774"",
                city: ""New Haven"",
                state: ""IN""
              },
              {
                zip: ""47965"",
                city: ""New Market"",
                state: ""IN""
              },
              {
                zip: ""47160"",
                city: ""New Middletown"",
                state: ""IN""
              },
              {
                zip: ""46163"",
                city: ""New Palestine"",
                state: ""IN""
              },
              {
                zip: ""46553"",
                city: ""New Paris"",
                state: ""IN""
              },
              {
                zip: ""47263"",
                city: ""New Point"",
                state: ""IN""
              },
              {
                zip: ""47967"",
                city: ""New Richmond"",
                state: ""IN""
              },
              {
                zip: ""47968"",
                city: ""New Ross"",
                state: ""IN""
              },
              {
                zip: ""47161"",
                city: ""New Salisbury"",
                state: ""IN""
              },
              {
                zip: ""47035"",
                city: ""New Trenton"",
                state: ""IN""
              },
              {
                zip: ""47162"",
                city: ""New Washington"",
                state: ""IN""
              },
              {
                zip: ""46961"",
                city: ""New Waverly"",
                state: ""IN""
              },
              {
                zip: ""47449"",
                city: ""Newberry"",
                state: ""IN""
              },
              {
                zip: ""47630"",
                city: ""Newburgh"",
                state: ""IN""
              },
              {
                zip: ""47966"",
                city: ""Newport"",
                state: ""IN""
              },
              {
                zip: ""47969"",
                city: ""Newtown"",
                state: ""IN""
              },
              {
                zip: ""46164"",
                city: ""Nineveh"",
                state: ""IN""
              },
              {
                zip: ""46060"",
                city: ""Noblesville"",
                state: ""IN""
              },
              {
                zip: ""46062"",
                city: ""Noblesville"",
                state: ""IN""
              },
              {
                zip: ""47264"",
                city: ""Norman"",
                state: ""IN""
              },
              {
                zip: ""46366"",
                city: ""North Judson"",
                state: ""IN""
              },
              {
                zip: ""46554"",
                city: ""North Liberty"",
                state: ""IN""
              },
              {
                zip: ""46962"",
                city: ""North Manchester"",
                state: ""IN""
              },
              {
                zip: ""46165"",
                city: ""North Salem"",
                state: ""IN""
              },
              {
                zip: ""47265"",
                city: ""North Vernon"",
                state: ""IN""
              },
              {
                zip: ""46555"",
                city: ""North Webster"",
                state: ""IN""
              },
              {
                zip: ""46556"",
                city: ""Notre Dame"",
                state: ""IN""
              },
              {
                zip: ""47660"",
                city: ""Oakland City"",
                state: ""IN""
              },
              {
                zip: ""47561"",
                city: ""Oaktown"",
                state: ""IN""
              },
              {
                zip: ""47367"",
                city: ""Oakville"",
                state: ""IN""
              },
              {
                zip: ""47562"",
                city: ""Odon"",
                state: ""IN""
              },
              {
                zip: ""47036"",
                city: ""Oldenburg"",
                state: ""IN""
              },
              {
                zip: ""46967"",
                city: ""Onward"",
                state: ""IN""
              },
              {
                zip: ""47451"",
                city: ""Oolitic"",
                state: ""IN""
              },
              {
                zip: ""46968"",
                city: ""Ora"",
                state: ""IN""
              },
              {
                zip: ""46063"",
                city: ""Orestes"",
                state: ""IN""
              },
              {
                zip: ""46776"",
                city: ""Orland"",
                state: ""IN""
              },
              {
                zip: ""47452"",
                city: ""Orleans"",
                state: ""IN""
              },
              {
                zip: ""46561"",
                city: ""Osceola"",
                state: ""IN""
              },
              {
                zip: ""47037"",
                city: ""Osgood"",
                state: ""IN""
              },
              {
                zip: ""46777"",
                city: ""Ossian"",
                state: ""IN""
              },
              {
                zip: ""47163"",
                city: ""Otisco"",
                state: ""IN""
              },
              {
                zip: ""47970"",
                city: ""Otterbein"",
                state: ""IN""
              },
              {
                zip: ""47564"",
                city: ""Otwell"",
                state: ""IN""
              },
              {
                zip: ""47453"",
                city: ""Owensburg"",
                state: ""IN""
              },
              {
                zip: ""47665"",
                city: ""Owensville"",
                state: ""IN""
              },
              {
                zip: ""47971"",
                city: ""Oxford"",
                state: ""IN""
              },
              {
                zip: ""47164"",
                city: ""Palmyra"",
                state: ""IN""
              },
              {
                zip: ""47454"",
                city: ""Paoli"",
                state: ""IN""
              },
              {
                zip: ""46166"",
                city: ""Paragon"",
                state: ""IN""
              },
              {
                zip: ""47270"",
                city: ""Paris Crossing"",
                state: ""IN""
              },
              {
                zip: ""47368"",
                city: ""Parker City"",
                state: ""IN""
              },
              {
                zip: ""47666"",
                city: ""Patoka"",
                state: ""IN""
              },
              {
                zip: ""47455"",
                city: ""Patricksburg"",
                state: ""IN""
              },
              {
                zip: ""47038"",
                city: ""Patriot"",
                state: ""IN""
              },
              {
                zip: ""47865"",
                city: ""Paxton"",
                state: ""IN""
              },
              {
                zip: ""47165"",
                city: ""Pekin"",
                state: ""IN""
              },
              {
                zip: ""46064"",
                city: ""Pendleton"",
                state: ""IN""
              },
              {
                zip: ""47369"",
                city: ""Pennville"",
                state: ""IN""
              },
              {
                zip: ""47974"",
                city: ""Perrysville"",
                state: ""IN""
              },
              {
                zip: ""46970"",
                city: ""Peru"",
                state: ""IN""
              },
              {
                zip: ""47567"",
                city: ""Petersburg"",
                state: ""IN""
              },
              {
                zip: ""46562"",
                city: ""Pierceton"",
                state: ""IN""
              },
              {
                zip: ""47866"",
                city: ""Pimento"",
                state: ""IN""
              },
              {
                zip: ""47975"",
                city: ""Pine Village"",
                state: ""IN""
              },
              {
                zip: ""46167"",
                city: ""Pittsboro"",
                state: ""IN""
              },
              {
                zip: ""46168"",
                city: ""Plainfield"",
                state: ""IN""
              },
              {
                zip: ""47568"",
                city: ""Plainville"",
                state: ""IN""
              },
              {
                zip: ""46779"",
                city: ""Pleasant Lake"",
                state: ""IN""
              },
              {
                zip: ""46563"",
                city: ""Plymouth"",
                state: ""IN""
              },
              {
                zip: ""47868"",
                city: ""Poland"",
                state: ""IN""
              },
              {
                zip: ""46781"",
                city: ""Poneto"",
                state: ""IN""
              },
              {
                zip: ""46368"",
                city: ""Portage"",
                state: ""IN""
              },
              {
                zip: ""47371"",
                city: ""Portland"",
                state: ""IN""
              },
              {
                zip: ""47633"",
                city: ""Poseyville"",
                state: ""IN""
              },
              {
                zip: ""47869"",
                city: ""Prairie Creek"",
                state: ""IN""
              },
              {
                zip: ""47670"",
                city: ""Princeton"",
                state: ""IN""
              },
              {
                zip: ""47456"",
                city: ""Quincy"",
                state: ""IN""
              },
              {
                zip: ""47166"",
                city: ""Ramsey"",
                state: ""IN""
              },
              {
                zip: ""47373"",
                city: ""Redkey"",
                state: ""IN""
              },
              {
                zip: ""46171"",
                city: ""Reelsville"",
                state: ""IN""
              },
              {
                zip: ""47977"",
                city: ""Remington"",
                state: ""IN""
              },
              {
                zip: ""47978"",
                city: ""Rensselaer"",
                state: ""IN""
              },
              {
                zip: ""47980"",
                city: ""Reynolds"",
                state: ""IN""
              },
              {
                zip: ""47634"",
                city: ""Richland"",
                state: ""IN""
              },
              {
                zip: ""47374"",
                city: ""Richmond"",
                state: ""IN""
              },
              {
                zip: ""47380"",
                city: ""Ridgeville"",
                state: ""IN""
              },
              {
                zip: ""47871"",
                city: ""Riley"",
                state: ""IN""
              },
              {
                zip: ""47040"",
                city: ""Rising Sun"",
                state: ""IN""
              },
              {
                zip: ""46172"",
                city: ""Roachdale"",
                state: ""IN""
              },
              {
                zip: ""46974"",
                city: ""Roann"",
                state: ""IN""
              },
              {
                zip: ""46783"",
                city: ""Roanoke"",
                state: ""IN""
              },
              {
                zip: ""46975"",
                city: ""Rochester"",
                state: ""IN""
              },
              {
                zip: ""47635"",
                city: ""Rockport"",
                state: ""IN""
              },
              {
                zip: ""47872"",
                city: ""Rockville"",
                state: ""IN""
              },
              {
                zip: ""46371"",
                city: ""Rolling Prairie"",
                state: ""IN""
              },
              {
                zip: ""47574"",
                city: ""Rome"",
                state: ""IN""
              },
              {
                zip: ""46784"",
                city: ""Rome City"",
                state: ""IN""
              },
              {
                zip: ""47981"",
                city: ""Romney"",
                state: ""IN""
              },
              {
                zip: ""47874"",
                city: ""Rosedale"",
                state: ""IN""
              },
              {
                zip: ""46065"",
                city: ""Rossville"",
                state: ""IN""
              },
              {
                zip: ""46978"",
                city: ""Royal Center"",
                state: ""IN""
              },
              {
                zip: ""46173"",
                city: ""Rushville"",
                state: ""IN""
              },
              {
                zip: ""46175"",
                city: ""Russellville"",
                state: ""IN""
              },
              {
                zip: ""46979"",
                city: ""Russiaville"",
                state: ""IN""
              },
              {
                zip: ""47575"",
                city: ""Saint Anthony"",
                state: ""IN""
              },
              {
                zip: ""47576"",
                city: ""Saint Croix"",
                state: ""IN""
              },
              {
                zip: ""46785"",
                city: ""Saint Joe"",
                state: ""IN""
              },
              {
                zip: ""46373"",
                city: ""Saint John"",
                state: ""IN""
              },
              {
                zip: ""47876"",
                city: ""Saint Mary Of The Woods"",
                state: ""IN""
              },
              {
                zip: ""47577"",
                city: ""Saint Meinrad"",
                state: ""IN""
              },
              {
                zip: ""47272"",
                city: ""Saint Paul"",
                state: ""IN""
              },
              {
                zip: ""47381"",
                city: ""Salamonia"",
                state: ""IN""
              },
              {
                zip: ""47167"",
                city: ""Salem"",
                state: ""IN""
              },
              {
                zip: ""46374"",
                city: ""San Pierre"",
                state: ""IN""
              },
              {
                zip: ""47578"",
                city: ""Sandborn"",
                state: ""IN""
              },
              {
                zip: ""47579"",
                city: ""Santa Claus"",
                state: ""IN""
              },
              {
                zip: ""47382"",
                city: ""Saratoga"",
                state: ""IN""
              },
              {
                zip: ""46375"",
                city: ""Schererville"",
                state: ""IN""
              },
              {
                zip: ""46376"",
                city: ""Schneider"",
                state: ""IN""
              },
              {
                zip: ""47580"",
                city: ""Schnellville"",
                state: ""IN""
              },
              {
                zip: ""47273"",
                city: ""Scipio"",
                state: ""IN""
              },
              {
                zip: ""47457"",
                city: ""Scotland"",
                state: ""IN""
              },
              {
                zip: ""47170"",
                city: ""Scottsburg"",
                state: ""IN""
              },
              {
                zip: ""47172"",
                city: ""Sellersburg"",
                state: ""IN""
              },
              {
                zip: ""47383"",
                city: ""Selma"",
                state: ""IN""
              },
              {
                zip: ""47274"",
                city: ""Seymour"",
                state: ""IN""
              },
              {
                zip: ""46068"",
                city: ""Sharpsville"",
                state: ""IN""
              },
              {
                zip: ""47879"",
                city: ""Shelburn"",
                state: ""IN""
              },
              {
                zip: ""46377"",
                city: ""Shelby"",
                state: ""IN""
              },
              {
                zip: ""46176"",
                city: ""Shelbyville"",
                state: ""IN""
              },
              {
                zip: ""47880"",
                city: ""Shepardsville"",
                state: ""IN""
              },
              {
                zip: ""46069"",
                city: ""Sheridan"",
                state: ""IN""
              },
              {
                zip: ""46565"",
                city: ""Shipshewana"",
                state: ""IN""
              },
              {
                zip: ""47384"",
                city: ""Shirley"",
                state: ""IN""
              },
              {
                zip: ""47581"",
                city: ""Shoals"",
                state: ""IN""
              },
              {
                zip: ""46982"",
                city: ""Silver Lake"",
                state: ""IN""
              },
              {
                zip: ""47458"",
                city: ""Smithville"",
                state: ""IN""
              },
              {
                zip: ""47459"",
                city: ""Solsberry"",
                state: ""IN""
              },
              {
                zip: ""46984"",
                city: ""Somerset"",
                state: ""IN""
              },
              {
                zip: ""47683"",
                city: ""Somerville"",
                state: ""IN""
              },
              {
                zip: ""46601"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46613"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46614"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46615"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46616"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46617"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46619"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46628"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46635"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46637"",
                city: ""South Bend"",
                state: ""IN""
              },
              {
                zip: ""46786"",
                city: ""South Milford"",
                state: ""IN""
              },
              {
                zip: ""46787"",
                city: ""South Whitley"",
                state: ""IN""
              },
              {
                zip: ""47460"",
                city: ""Spencer"",
                state: ""IN""
              },
              {
                zip: ""46788"",
                city: ""Spencerville"",
                state: ""IN""
              },
              {
                zip: ""47385"",
                city: ""Spiceland"",
                state: ""IN""
              },
              {
                zip: ""47386"",
                city: ""Springport"",
                state: ""IN""
              },
              {
                zip: ""47462"",
                city: ""Springville"",
                state: ""IN""
              },
              {
                zip: ""47584"",
                city: ""Spurgeon"",
                state: ""IN""
              },
              {
                zip: ""46985"",
                city: ""Star City"",
                state: ""IN""
              },
              {
                zip: ""47982"",
                city: ""State Line"",
                state: ""IN""
              },
              {
                zip: ""47881"",
                city: ""Staunton"",
                state: ""IN""
              },
              {
                zip: ""47585"",
                city: ""Stendal"",
                state: ""IN""
              },
              {
                zip: ""46180"",
                city: ""Stilesville"",
                state: ""IN""
              },
              {
                zip: ""47464"",
                city: ""Stinesville"",
                state: ""IN""
              },
              {
                zip: ""47983"",
                city: ""Stockwell"",
                state: ""IN""
              },
              {
                zip: ""47387"",
                city: ""Straughn"",
                state: ""IN""
              },
              {
                zip: ""47882"",
                city: ""Sullivan"",
                state: ""IN""
              },
              {
                zip: ""47388"",
                city: ""Sulphur Springs"",
                state: ""IN""
              },
              {
                zip: ""46379"",
                city: ""Sumava Resorts"",
                state: ""IN""
              },
              {
                zip: ""46070"",
                city: ""Summitville"",
                state: ""IN""
              },
              {
                zip: ""47041"",
                city: ""Sunman"",
                state: ""IN""
              },
              {
                zip: ""46986"",
                city: ""Swayzee"",
                state: ""IN""
              },
              {
                zip: ""46987"",
                city: ""Sweetser"",
                state: ""IN""
              },
              {
                zip: ""47465"",
                city: ""Switz City"",
                state: ""IN""
              },
              {
                zip: ""46567"",
                city: ""Syracuse"",
                state: ""IN""
              },
              {
                zip: ""47175"",
                city: ""Taswell"",
                state: ""IN""
              },
              {
                zip: ""47280"",
                city: ""Taylorsville"",
                state: ""IN""
              },
              {
                zip: ""47586"",
                city: ""Tell City"",
                state: ""IN""
              },
              {
                zip: ""47637"",
                city: ""Tennyson"",
                state: ""IN""
              },
              {
                zip: ""47802"",
                city: ""Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""47803"",
                city: ""Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""47804"",
                city: ""Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""47805"",
                city: ""Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""47807"",
                city: ""Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""47809"",
                city: ""Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""46381"",
                city: ""Thayer"",
                state: ""IN""
              },
              {
                zip: ""46071"",
                city: ""Thorntown"",
                state: ""IN""
              },
              {
                zip: ""46570"",
                city: ""Tippecanoe"",
                state: ""IN""
              },
              {
                zip: ""46072"",
                city: ""Tipton"",
                state: ""IN""
              },
              {
                zip: ""46571"",
                city: ""Topeka"",
                state: ""IN""
              },
              {
                zip: ""46181"",
                city: ""Trafalgar"",
                state: ""IN""
              },
              {
                zip: ""47588"",
                city: ""Troy"",
                state: ""IN""
              },
              {
                zip: ""47467"",
                city: ""Tunnelton"",
                state: ""IN""
              },
              {
                zip: ""46988"",
                city: ""Twelve Mile"",
                state: ""IN""
              },
              {
                zip: ""47177"",
                city: ""Underwood"",
                state: ""IN""
              },
              {
                zip: ""47390"",
                city: ""Union City"",
                state: ""IN""
              },
              {
                zip: ""46382"",
                city: ""Union Mills"",
                state: ""IN""
              },
              {
                zip: ""46791"",
                city: ""Uniondale"",
                state: ""IN""
              },
              {
                zip: ""47468"",
                city: ""Unionville"",
                state: ""IN""
              },
              {
                zip: ""47884"",
                city: ""Universal"",
                state: ""IN""
              },
              {
                zip: ""46989"",
                city: ""Upland"",
                state: ""IN""
              },
              {
                zip: ""46990"",
                city: ""Urbana"",
                state: ""IN""
              },
              {
                zip: ""47281"",
                city: ""Vallonia"",
                state: ""IN""
              },
              {
                zip: ""46383"",
                city: ""Valparaiso"",
                state: ""IN""
              },
              {
                zip: ""46385"",
                city: ""Valparaiso"",
                state: ""IN""
              },
              {
                zip: ""46991"",
                city: ""Van Buren"",
                state: ""IN""
              },
              {
                zip: ""47987"",
                city: ""Veedersburg"",
                state: ""IN""
              },
              {
                zip: ""47590"",
                city: ""Velpen"",
                state: ""IN""
              },
              {
                zip: ""47282"",
                city: ""Vernon"",
                state: ""IN""
              },
              {
                zip: ""47042"",
                city: ""Versailles"",
                state: ""IN""
              },
              {
                zip: ""47043"",
                city: ""Vevay"",
                state: ""IN""
              },
              {
                zip: ""47591"",
                city: ""Vincennes"",
                state: ""IN""
              },
              {
                zip: ""46992"",
                city: ""Wabash"",
                state: ""IN""
              },
              {
                zip: ""47638"",
                city: ""Wadesville"",
                state: ""IN""
              },
              {
                zip: ""46573"",
                city: ""Wakarusa"",
                state: ""IN""
              },
              {
                zip: ""46182"",
                city: ""Waldron"",
                state: ""IN""
              },
              {
                zip: ""46574"",
                city: ""Walkerton"",
                state: ""IN""
              },
              {
                zip: ""46994"",
                city: ""Walton"",
                state: ""IN""
              },
              {
                zip: ""46390"",
                city: ""Wanatah"",
                state: ""IN""
              },
              {
                zip: ""46792"",
                city: ""Warren"",
                state: ""IN""
              },
              {
                zip: ""46580"",
                city: ""Warsaw"",
                state: ""IN""
              },
              {
                zip: ""46582"",
                city: ""Warsaw"",
                state: ""IN""
              },
              {
                zip: ""47501"",
                city: ""Washington"",
                state: ""IN""
              },
              {
                zip: ""46793"",
                city: ""Waterloo"",
                state: ""IN""
              },
              {
                zip: ""47989"",
                city: ""Waveland"",
                state: ""IN""
              },
              {
                zip: ""46794"",
                city: ""Wawaka"",
                state: ""IN""
              },
              {
                zip: ""47990"",
                city: ""Waynetown"",
                state: ""IN""
              },
              {
                zip: ""47469"",
                city: ""West Baden Springs"",
                state: ""IN""
              },
              {
                zip: ""47003"",
                city: ""West College Corner"",
                state: ""IN""
              },
              {
                zip: ""47060"",
                city: ""West Harrison"",
                state: ""IN""
              },
              {
                zip: ""47906"",
                city: ""West Lafayette"",
                state: ""IN""
              },
              {
                zip: ""47907"",
                city: ""West Lafayette"",
                state: ""IN""
              },
              {
                zip: ""47991"",
                city: ""West Lebanon"",
                state: ""IN""
              },
              {
                zip: ""46183"",
                city: ""West Newton"",
                state: ""IN""
              },
              {
                zip: ""47885"",
                city: ""West Terre Haute"",
                state: ""IN""
              },
              {
                zip: ""46074"",
                city: ""Westfield"",
                state: ""IN""
              },
              {
                zip: ""47596"",
                city: ""Westphalia"",
                state: ""IN""
              },
              {
                zip: ""47992"",
                city: ""Westpoint"",
                state: ""IN""
              },
              {
                zip: ""47283"",
                city: ""Westport"",
                state: ""IN""
              },
              {
                zip: ""46391"",
                city: ""Westville"",
                state: ""IN""
              },
              {
                zip: ""46392"",
                city: ""Wheatfield"",
                state: ""IN""
              },
              {
                zip: ""47597"",
                city: ""Wheatland"",
                state: ""IN""
              },
              {
                zip: ""46393"",
                city: ""Wheeler"",
                state: ""IN""
              },
              {
                zip: ""46184"",
                city: ""Whiteland"",
                state: ""IN""
              },
              {
                zip: ""46075"",
                city: ""Whitestown"",
                state: ""IN""
              },
              {
                zip: ""46394"",
                city: ""Whiting"",
                state: ""IN""
              },
              {
                zip: ""46186"",
                city: ""Wilkinson"",
                state: ""IN""
              },
              {
                zip: ""47470"",
                city: ""Williams"",
                state: ""IN""
              },
              {
                zip: ""47393"",
                city: ""Williamsburg"",
                state: ""IN""
              },
              {
                zip: ""47993"",
                city: ""Williamsport"",
                state: ""IN""
              },
              {
                zip: ""46996"",
                city: ""Winamac"",
                state: ""IN""
              },
              {
                zip: ""47394"",
                city: ""Winchester"",
                state: ""IN""
              },
              {
                zip: ""46076"",
                city: ""Windfall"",
                state: ""IN""
              },
              {
                zip: ""47994"",
                city: ""Wingate"",
                state: ""IN""
              },
              {
                zip: ""46590"",
                city: ""Winona Lake"",
                state: ""IN""
              },
              {
                zip: ""47598"",
                city: ""Winslow"",
                state: ""IN""
              },
              {
                zip: ""47995"",
                city: ""Wolcott"",
                state: ""IN""
              },
              {
                zip: ""46795"",
                city: ""Wolcottville"",
                state: ""IN""
              },
              {
                zip: ""46797"",
                city: ""Woodburn"",
                state: ""IN""
              },
              {
                zip: ""47471"",
                city: ""Worthington"",
                state: ""IN""
              },
              {
                zip: ""46595"",
                city: ""Wyatt"",
                state: ""IN""
              },
              {
                zip: ""47997"",
                city: ""Yeoman"",
                state: ""IN""
              },
              {
                zip: ""46798"",
                city: ""Yoder"",
                state: ""IN""
              },
              {
                zip: ""47396"",
                city: ""Yorktown"",
                state: ""IN""
              },
              {
                zip: ""46998"",
                city: ""Young America"",
                state: ""IN""
              },
              {
                zip: ""46799"",
                city: ""Zanesville"",
                state: ""IN""
              },
              {
                zip: ""46077"",
                city: ""Zionsville"",
                state: ""IN""
              }
]";
            _items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CityStateZip>>(json);
        }
    }
}
