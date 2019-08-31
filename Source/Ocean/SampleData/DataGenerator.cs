namespace Oceanware.Ocean.SampleData {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Class DataGenerator, used to create sample data at design-time, test-time, or run-time
    /// </summary>
    public sealed class DataGenerator {
        const String _stringWhiteSpace = " ";
        readonly List<String> _cities = new List<String> { "Cranbury", "New York", "Sofia", "London", "Paris", "Redmond", "Washington, D.C.", "Melville", "Montevideo", "Bangalore", "Tokyo", "Shibuya" };
        readonly List<String> _countries = new List<String> { "United States", "Japan", "Bulgaria", "Uruguay", "United Kingdom", "India" };
        private readonly List<String> _emailProviders = new List<String> { "gmail.com", "outlook.com", "hotmail.com", "zohomail.com", "yahoo.com", "mail.com", "gmx.com", "pronto.com" };
        private readonly List<String> _femaleFirstNames = new List<String> { "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Maria", "Susan", "Margaret", "Dorothy", "Lisa", "Nancy", "Karen", "Betty", "Helen", "Sandra", "Donna", "Carol", "Ruth", "Sharon", "Michelle", "Laura", "Sarah", "Kimberly", "Deborah", "Jessica", "Shirley", "Cynthia", "Angela", "Melissa", "Brenda", "Amy", "Anna", "Rebecca", "Virginia", "Kathleen", "Pamela", "Martha", "Debra", "Amanda", "Stephanie", "Carolyn", "Christine", "Marie", "Janet", "Catherine", "Frances", "Ann", "Joyce", "Diane", "Alice", "Julie", "Heather", "Teresa", "Doris", "Gloria", "Evelyn", "Jean", "Cheryl", "Mildred", "Katherine", "Joan", "Ashley", "Judith", "Rose", "Janice", "Kelly", "Nicole", "Judy", "Christina", "Kathy", "Theresa", "Beverly", "Denise", "Tammy", "Irene", "Jane", "Lori", "Rachel", "Marilyn", "Andrea", "Kathryn", "Louise", "Sara", "Anne", "Jacqueline", "Wanda", "Bonnie", "Julia", "Ruby", "Lois", "Tina", "Phyllis", "Norma", "Paula", "Diana", "Annie", "Lillian", "Emily", "Robin", "Peggy", "Crystal", "Gladys", "Rita", "Dawn", "Connie", "Florence", "Tracy", "Edna", "Tiffany", "Carmen", "Rosa", "Cindy", "Grace", "Wendy", "Victoria", "Edith", "Kim", "Sherry", "Sylvia", "Josephine", "Thelma", "Shannon", "Sheila", "Ethel", "Ellen", "Elaine", "Marjorie", "Carrie", "Charlotte", "Monica", "Esther", "Pauline", "Emma", "Juanita", "Anita", "Rhonda", "Hazel", "Amber", "Eva", "Debbie", "April", "Leslie", "Clara", "Lucille", "Jamie", "Joanne", "Eleanor", "Valerie", "Danielle", "Megan", "Alicia", "Suzanne", "Michele", "Gail", "Bertha", "Darlene", "Veronica", "Jill", "Erin", "Geraldine", "Lauren", "Cathy", "Joann", "Lorraine", "Lynn", "Sally", "Regina", "Erica", "Beatrice", "Dolores", "Bernice", "Audrey", "Yvonne", "Annette", "June", "Samantha", "Marion", "Dana", "Stacy", "Ana", "Renee", "Ida", "Vivian", "Roberta", "Holly", "Brittany", "Melanie", "Loretta", "Yolanda", "Jeanette", "Laurie", "Katie", "Kristen", "Vanessa", "Alma", "Sue", "Elsie", "Beth", "Jeanne", "Vicki", "Carla", "Tara", "Rosemary", "Eileen", "Terri", "Gertrude", "Lucy", "Tonya", "Ella", "Stacey", "Wilma", "Gina", "Kristin", "Jessie", "Natalie", "Agnes", "Vera", "Willie", "Charlene", "Bessie", "Delores", "Melinda", "Pearl", "Arlene", "Maureen", "Colleen", "Allison", "Tamara", "Joy", "Georgia", "Constance", "Lillie", "Claudia", "Jackie", "Marcia", "Tanya", "Nellie", "Minnie", "Marlene", "Heidi", "Glenda", "Lydia", "Viola", "Courtney", "Marian", "Stella", "Caroline", "Dora", "Jo", "Vickie", "Mattie", "Terry", "Maxine", "Irma", "Mabel", "Marsha", "Myrtle", "Lena", "Christy", "Deanna", "Patsy", "Hilda", "Gwendolyn", "Jennie", "Nora", "Margie", "Nina", "Cassandra", "Leah", "Penny", "Kay", "Priscilla", "Naomi", "Carole", "Brandy", "Olga", "Billie", "Dianne", "Tracey", "Leona", "Jenny", "Felicia", "Sonia", "Miriam", "Velma", "Becky", "Bobbie", "Violet", "Kristina", "Toni", "Misty", "Mae", "Shelly", "Daisy", "Ramona", "Sherri", "Erika", "Katrina", "Claire", "Lindsey", "Lindsay", "Geneva", "Guadalupe", "Belinda", "Margarita", "Sheryl", "Cora", "Faye", "Ada", "Natasha", "Sabrina", "Isabel", "Marguerite", "Hattie", "Harriet", "Molly", "Cecilia", "Kristi", "Brandi", "Blanche", "Sandy", "Rosie", "Joanna", "Iris", "Eunice", "Angie", "Inez", "Lynda", "Madeline", "Amelia", "Alberta", "Genevieve", "Monique", "Jodi", "Janie", "Maggie", "Kayla", "Sonya", "Jan", "Lee", "Kristine", "Candace", "Fannie", "Maryann", "Opal", "Alison", "Yvette", "Melody", "Luz", "Susie", "Olivia", "Flora", "Shelley", "Kristy", "Mamie", "Lula", "Lola", "Verna", "Beulah", "Antoinette", "Candice", "Juana", "Jeannette", "Pam", "Kelli", "Hannah", "Whitney", "Bridget", "Karla", "Celia", "Latoya", "Patty", "Shelia", "Gayle", "Della", "Vicky", "Lynne", "Sheri", "Marianne", "Kara", "Jacquelyn", "Erma", "Blanca", "Myra", "Leticia", "Pat", "Krista", "Roxanne", "Angelica", "Johnnie", "Robyn", "Francis", "Adrienne", "Rosalie", "Alexandra", "Brooke", "Bethany", "Sadie", "Bernadette", "Traci", "Jody", "Kendra", "Jasmine", "Nichole", "Rachael", "Chelsea", "Mable", "Ernestine", "Muriel", "Marcella", "Elena", "Krystal", "Angelina", "Nadine", "Kari", "Estelle", "Dianna", "Paulette", "Lora", "Mona", "Doreen", "Rosemarie", "Angel", "Desiree", "Antonia", "Hope", "Ginger", "Janis", "Betsy", "Christie", "Freda", "Mercedes", "Meredith", "Lynette", "Teri", "Cristina", "Eula", "Leigh", "Meghan", "Sophia", "Eloise", "Rochelle", "Gretchen", "Cecelia", "Raquel", "Henrietta", "Alyssa", "Jana", "Kelley", "Gwen", "Kerry", "Jenna", "Tricia", "Laverne", "Olive", "Alexis", "Tasha", "Silvia", "Elvira", "Casey", "Delia", "Sophie", "Kate", "Patti", "Lorena", "Kellie", "Sonja", "Lila", "Lana", "Darla", "May", "Mindy", "Essie", "Mandy", "Lorene", "Elsa", "Josefina", "Jeannie", "Miranda", "Dixie", "Lucia", "Marta", "Faith", "Lela", "Johanna", "Shari", "Camille", "Tami", "Shawna", "Elisa", "Ebony", "Melba", "Ora", "Nettie", "Tabitha", "Ollie", "Jaime", "Winifred", "Kristie" };
        private readonly List<String> _lastNames = new List<String> { "Hettinger", "Bauschke", "Marner", "Mathies", "Neubert", "Norris", "Sollner", "Hodkiewicz", "Dienel", "Hasse", "Bauch", "Haley", "Apitz", "Daubner", "Jakubczyk", "Malucha", "Raabe", "Rohrer", "Sipes", "Wright", "Frauendorf", "Grothkopp", "Herold", "Roschinsky", "Saumweber", "Bayer", "Bins", "Koepp", "Aschenbroich", "Breu", "Klemme", "Kowalick", "Krause", "Johnson", "Hanenberger", "Ihly", "Kazmarek", "Muckenthaler", "Volkman", "Wilderman", "Karhoff", "Kwadwo", "Nolte", "Porsche", "Wittl", "Roob", "Cornelsen", "Eberhardt", "Kulma", "Maczey", "Maurer", "Rach", "Rother", "Schondelmaier", "Strohschank", "Tivontschik", "Urban", "Kuvalis", "Lubowitz", "Jackson", "Ade", "Weight", "Cummerata", "Rohan", "Windler", "Baumeister", "Greb", "Hepperle", "Schwidde", "Baumbach", "Turner", "Dolch", "Dragu", "Gromisch", "Keiner", "Noack", "Rheder", "Roloff", "Skibicki", "Crist", "Kohler", "Astafei", "Beckmann", "Blume", "Gierisch", "Konow", "Lg", "Moller", "Mues", "Prange", "Schima", "Wichmann", "Von", "Wunsch", "Aigner", "Ghirmai", "Jaros", "Karrass", "Kúhn", "Szendrei", "Werrmann", "Wimmer", "Glover", "Zieme", "Rees", "Buchholz", "Helm", "Lenzen", "Lepthin", "Nabein", "Rosenauer", "Schildhauer", "Lee", "Young", "Gehre", "Hildenbrand", "Koszewski", "Kuhnen", "Rentz", "Ryzih", "Sillah", "Feest", "Johns", "Kris", "Price", "Beggerow", "Breitenstein", "Doskoczynski", "Gottschalk", "Habel", "Kleiss", "Mehlorn", "Trampeli", "Urbaniak", "Nikolaus", "Morrison", "Bielert", "Kappler", "Lubina", "Mensing", "Moede", "Radtke", "Schmidtchen", "Schwennen", "DuBuque", "Macejkovic", "Nolan", "O'Connell", "O'Hara", "Ortiz", "Brauer", "Crosskofp", "Emert", "Gabler", "Gunkel", "Hadwich", "Welz", "Wessinghage", "Hehl", "Lax", "Lipus", "Pagac", "Mcdermott", "Baseda", "Caspari", "Jambor", "Kolokas", "Kuhlee", "Schuri", "Schwanbeck", "Tiedtke", "Tillack", "Tuschmo", "Carroll", "Prohaska", "Clarius", "Finke", "Schirmer", "Schuff", "Freigang", "Frosch", "Kohrt", "Stengel", "Vontein", "Armstrong", "Johnston", "Ritchie", "Buder", "Erlei", "Hajek", "Kurnicki", "Ochs", "Pfingsten", "Plass", "Zimmermann", "Zinser", "Brakus", "Nicolas", "Hense", "Kick", "Lehmann", "Peters", "Strutz", "Carter", "Robinson", "Bigdeli", "Denner", "Lamos", "Tucholke", "Borer", "Nader", "Schaefer", "Nau", "Nodler", "Paukner", "Zeyen", "Miller", "Padberg", "Reichert", "Welch", "Kampschulte", "Pottel", "Tischler", "McKenzie", "Stanton", "Wilson", "Mclaughlin", "Forkel", "Fricke", "Leide", "May", "Neumann", "Rehwagen", "Schersing", "Williamson", "Clark", "Beh", "Beushausen", "Emmelmann", "Thust", "Weniger", "Erdman", "VonRueden", "Brosch", "Ender", "Grimm", "Tschirch", "Vieweg", "Anderson", "Barton", "Corwin", "Cronin", "Sauer", "Tromp", "Beutelspacher", "Kallabis", "Kempter", "Abshire", "Moss", "Gunther", "Jegorov", "Kloss", "Kral", "Laux", "Neubauer", "Green", "Hilpert", "Stiedemann", "Weissnat", "Bunjes", "Huebel", "Kupfer", "Linke", "Oppong", "Hirsch", "Ibe", "Niedermeier", "Stauss", "Cartwright", "Hane", "Hoeger", "Cox", "Illing", "Kleinmann", "Rosbach", "Timmermann", "Vogt", "Winter", "Considine", "Kuphal", "Ruecker", "Diekmann", "Graf", "Keil", "Kowalinski", "Laws", "Mordhorst", "Rhoden", "Roleder", "Hecht", "Lufft", "Reus", "Reuss", "Streller", "Tremmel", "Zauber", "Gottlieb", "Franz", "Kozakiewicz", "Reusse", "Williams", "Ahrenberg", "Gruning", "Herweg", "Huth", "Loogen", "Ophey", "Wissing", "Larkin", "Mohr", "Murray", "Albert", "Cleem", "Jucken", "Tietze", "Mayert", "Purdy", "Thiel", "Weber", "Fleischmann", "Kempe", "Tress", "Wischek", "Watsica", "Manns", "Schacht", "Schonberg", "Steinmetz", "Becker", "Eich", "Holl", "Krieger", "Landmann", "Meisch", "Renk", "Vogel", "Bahringer", "Bender", "Benzing", "Brenner", "Prey", "Schott", "Wyludda", "Gorczany", "Gulgowski", "Rippin", "Bahl", "Balnuweit", "Bauschinger", "Brandis", "Tudow", "Bartell", "Bashirian", "Patel", "Bachmann", "Gamlin", "Hallmann", "Kustermann", "Orth", "Ringer", "Sailer", "Schwab", "Cassin", "Little", "Rutherford", "Albrecht", "Dohring", "Erm", "Hannecker", "Heiser", "Jung", "Lutje", "Stahl", "Terlecki", "Rogahn", "Decker", "Dreher", "Figura", "Huneke", "Neupert", "Schniedermeier", "Travan", "Wurm", "Zschunke", "Lakin", "Abel", "Hessek", "Koleiski", "Tilgner", "Verzi", "Funk", "Stark", "O'conner", "Denzinger", "Drees", "Goller", "Pitschugin", "Schwarz", "Harris", "Bruhns", "Ehmann", "Flore", "Garatva", "Kalinowski", "Kopf", "Sommer", "Stang", "Robel", "Rosenbaum", "Donie", "Langhirt", "Ritosek", "Siewert", "Goldner", "Gusikowski", "Tillman", "Wood", "Fahner", "Laurén", "Liebach", "Reinberg", "Sachse", "Homenick", "McGlynn", "Kelly", "Campbell", "Carlsohn", "Karst", "Merseburg", "Rapp", "Rossler", "Saile", "Savoia", "Herrmann", "Hohnheiser", "Kochan", "Koehler", "Motchebon", "Peselman", "Reiber", "Bouschen", "Daub", "Schmalzle", "Van", "Winkelmann", "Cruickshank", "Heaney", "Paucek", "Reynolds", "Wyman", "Fenner", "Hache", "Knut", "Lammert", "Maybach", "Moormann", "Ponitzsch", "Schwarzkopf", "Zipse", "Heidenreich", "Veum", "Golling", "Haaf", "Schindzielorz", "Steffen", "Okuneva", "Gastel", "Heberstreit", "Hohenberger", "Neuschwander", "Pajonk", "Zach", "Schmidt", "Benner", "Fust", "Gehrke", "Kahl", "Koczulla", "Vogelgsang", "Bergnaum", "Edwards", "Ahlke", "Beavogui", "Dies", "Engelen", "Janich", "Liebold", "Sahner", "Lemke", "Giesche", "Kesselring", "Kinzel", "Rahn", "Storp", "Suffa", "Sujew", "Zimmer", "Boyer", "Bauer", "Dahmen", "Mrugalla", "Nannen", "Scholz", "Auer", "Bruns", "Kufahl", "Morgenstern", "Sussmann", "Keebler", "Tremblay", "Gabius", "Helpling", "Kallensee", "Leist", "Tonat", "Wehrsen", "O'Kon", "Weimann", "Evans", "Grasse", "Linnenbaum", "Pinnock", "Restorff", "Sonn", "Tonn", "Harber", "Hintz", "Bienias", "Birkemeyer", "Kosenkow", "Renz", "Schedler", "Vey", "Fahey", "Kulas", "Marquardt", "Bultmann", "Ertl", "Gutschank", "Haug", "Jess", "Knorscheidt", "Michel", "Somssich", "Blanda", "Harvey", "Huel", "Mason", "Assmus", "Koderisch", "Scheurer", "Schmitt", "O'reilly", "Balck", "Grosser", "Hassfeld", "Jerschabek", "Swillims", "Balistreri", "Schinner", "Thompson", "Baarck", "Beele", "Benecke", "Feuerbach", "Fischer", "Umminger", "Gibson", "Kunze", "Wiza", "Abraham", "Balzer", "Griese", "Kerl", "Kern", "Lindenmayer", "Mockenhaupt", "Schimmer", "Östringer", "D'Amore", "Hessel", "Kshlerin", "Langosh", "Golomski", "Gruber", "Hess", "Jungton", "Weidner", "Weis", "Wollmann", "Heller", "Garden", "Busch", "Gerschler", "Krodinger", "Scherbarth", "Soboll", "Sporrer", "Zwiener", "Ahrens", "Dautzenberg", "Einert", "Gombert", "Herberger", "Ruth", "Schnieder", "Suthe", "Mayer", "Bedewitz", "Betz", "De", "Lokar", "Scharf", "Wujak", "Dold", "Friedek", "Honz", "Stepanov", "Stifel", "Walther", "Abernathy", "Aufderhar", "Cummings", "Kerluke", "Kiehn", "Legros", "Mann", "Medhurst", "Clarke", "Dingelstedt", "Hermiston", "Abt", "Buchrucker", "Jander", "Kinadeter", "Roba", "Seiders", "Collins", "Crona", "Farrell", "Kuhlman", "Derr", "Jansen", "Komoll", "Ladewig", "Mathes", "Orthmann", "Pingpank", "Ritz", "Delonge", "Freimuth", "Harms", "Krauspe", "Kulimann", "Stephan", "Dietz", "Gummelt", "Hering", "Kreutz", "Lange", "Lenz", "Mewes", "Rautenkranz", "Spinka", "Adam", "Dreissigacker", "Gehring", "Hedermann", "Kawohl", "Poschmann", "Ripken", "Schahbasian", "Stanger", "Steidl", "Zeuch", "Kuhn", "Balcer", "Gruszecki", "Dietrich", "Fritsch", "Powlowski", "Khan", "Brinkmann", "Konya", "Krippner", "Kuschmann", "Collier", "Mckenzie", "Crews", "Effler", "Filipowski", "Franzmann", "Fusenig", "Huckestein", "Kraft", "Letzelter", "Mauroff", "Northoff", "Vangermain", "Lynch", "Satterfield", "Barylla", "Gatzka", "Kirst", "Schmitz", "Hughes", "Cotthardt", "Ekpo", "Hennes", "Sielemann", "Teufel", "Vokuhl", "Weigel", "Barrows", "Boehm", "Jacobs", "Larson", "Mante", "Achkinadze", "Blank", "Daudrich", "Eleyth", "Kaufmann", "Krabbe", "Rangen", "Volker", "Rolfson", "Dobler", "Franke", "Lott", "Normann", "Rodehau", "Keller", "Owen", "Bos", "Frauen", "Hartwig", "Huke", "Knobel", "Knoll", "Plank", "Soller", "Steding", "Wierig", "Mraz", "O'Reilly", "Rau", "Barr", "Fehrig", "Hamann", "Haupt", "Marx", "Steinert", "Voigt", "West", "Dieckmann", "Fietz", "Gebhardt", "Nytra", "Osenberg", "Panzig", "Schellenbeck", "Moen", "Buss", "Conrad", "Elss", "Gaba", "Ganzmann", "Heinke", "Jarets", "Metzger", "Neurohr", "Reifenrath", "Roberts", "Trantow", "Wisoky", "Eisenlauer", "Figl", "Heinrich", "Zipp", "Heathcote", "Olson", "Hamilton", "Beck", "Kastner", "Lutz", "Maier", "Schottmann", "Spahn", "Brown", "Anding", "Konieczny", "Salzmann", "Schwarzer", "McDermott", "Streich", "Waelchi", "Frank", "Hanniske", "Hoffmann", "Reid", "Achilles", "Blaschek", "Dertmann", "Osei", "Sander", "Willwacher", "Bailey", "Berge", "Romaguera", "Paterson", "Brettschneider", "Camara", "Kurschilgen", "Schwartz", "Seibold", "Hahn", "Langworth", "Quinn", "Beyer", "Leyckes", "Mulrain", "Plotzitzka", "Terheiden", "Conn", "Wuckert", "Stewart", "Burghagen", "Freisen", "Schieskow", "Taufratshofer", "Ziegler", "Hentel", "Markowski", "Seitz", "Siemon", "Wilhelm", "Conroy", "Hayes", "Lind", "Turcotte", "White", "Kedzierski", "Klaas", "Siebert", "Sutschet", "Dauth", "Horak", "Koehl", "Marquart", "Rink", "Schmid", "Simonis", "Brandt", "Christ", "Gangnus", "Hechler", "Kausemann", "Runte", "Kluwe", "Tegethof", "Cormier", "Berning", "Gollnow", "Jacob", "Liebich", "Matula", "Schirrmeister", "Schwarzmeier", "Vater", "Bak", "Klauder", "Kupprion", "Pfennig", "Polizzi", "Trautmann", "Uhlig", "Kahlmeyer", "Kleinsteuber", "Neuendorf", "Sehls", "Sonnabend", "Spielvogel", "Vona", "Weyel", "Dickens", "Gutmann", "Reilly", "Hall", "Bolm", "Gamper", "Greschner", "Thieke", "Uliczka", "Ziemann", "O'connell", "Greithanner", "Heidler", "Pippig", "Wollenberg", "Bernhard", "Klein", "Leffler", "Greger", "Keutel", "Pietsch", "Poeschl", "Raubuch", "Uibel", "Wittich", "Daugherty", "Ross", "Dethloff", "Salzer", "Schwandke", "Hackett", "Oeser", "Schultz", "Smitham", "Cordes", "Jahn", "Kappe", "Thimm", "Rose", "Bickel", "Dreier", "Feller", "Fincke", "Friedrich", "Kaul", "Wilky", "Reichel", "Baganz", "Boruschewski", "Burger", "Knorr", "Niklaus", "Spinner", "Swift", "Dilla", "Esser", "Ghosh", "Leo", "Moritz", "Ostwald", "Ferry", "Mitchell", "Stracke", "Amann", "Heck", "Rokossa", "Dehmel", "Diezel", "Dylus", "Grossheim", "Kette", "Matschke", "Neimke", "Nerius", "Oberem", "Will", "Friedmann", "Hentschel", "Hooss", "Hort", "Metzner", "Moguenara", "Riedel", "Schorr", "Schramm", "Slotta", "Dach", "Halvorson", "Arendt", "Drechsler", "Haschke", "Heinemann", "Hudak", "Sagafe", "Scherer", "Splinter", "Straub", "Demut", "Goldkamp", "Hingst", "Schulz", "Feil", "Leuschke", "Fitschen", "Klose", "Reif", "Urbansky", "Strosin", "Baumann", "Lieder", "Olbrich", "Patzwahl", "Ringel", "Schultze", "Senkel", "Greenfelder", "Haag", "Da", "Duma", "Geese", "Grotke", "Holzdeppe", "Huhn", "Lewin", "Mallmann", "Reichardt", "Schielke", "Bode", "Ebert", "Kautzer", "Brand", "Hoffman", "Rodowski", "Stenzel", "Waldmann", "Grant", "Heidelmann", "Pflieger", "Ranftl", "Donnelly", "Jones", "Waters", "Hellmann", "Plauk", "Wakan", "Woytkowska", "Zanner", "Bergstrom", "Graham", "Klocko", "Wintheiser", "Faulhaber", "Gerbennow", "Haferkamp", "Klaus", "Thiomas", "Bogan", "Stokes", "Zboncak", "Klinger", "Kruschinski", "Logsch", "Schlawitz", "Wolf", "Battke", "Hensel", "Herbrand", "Hohl", "Jellinghaus", "Karus", "Kraus", "Walz", "Greenholt", "Schroeder", "Steuber", "Mertens", "Pallentin", "Wiechmann", "Bednar", "Marvin", "D'amore", "Bienasch", "Dahm", "Lorenz", "Wallstab", "Abramovic", "Bittner", "Borgschulze", "Brandenburg", "Gross", "Krueger", "Hilll", "Kub", "Gilde", "Goldfuss", "Mathiszik", "Nitzsche", "Balkow", "Florczak", "Kresse", "Kúhnert", "Loy", "Lukoschek", "Rieger", "Tsamonikian", "Ryan", "Hermecke", "Oschkenat", "Proske", "Schnelting", "Tepper", "Breitenberg", "Dooley", "Fisher", "Jakubowski", "Schmeler", "Thomas", "Bock", "Broening", "Dauer", "Ecker", "Herwig", "Kurrat", "Molitor", "Riester", "Runolfsdottir", "Schumm", "Yundt", "Esenwein", "Franzis", "Kahlert", "Lausecker", "Lohse", "Margis", "Mensah", "Pohl", "Krajcik", "Marks", "Abicht", "Cleve", "Floder", "Gelling", "Reimann", "Strege", "Ullmann", "Boxhammer", "Lischka", "Nwachukwu", "Ruch", "Streck", "Tittman", "Christiansen", "Kreiger", "Byrd", "Feld", "Frahmeke", "Janke", "Kohlmann", "Lohre", "Mielke", "Farber", "Kettenis", "Michallek", "Mohrhard", "Steuk", "Abbott", "Mosciski", "O'Conner", "Burkhardt", "Hiebl", "Huber", "Zapletal", "Emmerich", "Hartmann", "Pouros", "Rodriguez", "Franta", "Gadschiew", "Hauke", "Mintzlaff", "Otte", "Reichling", "Schrader", "Koss", "Rath", "Geissler", "Henry", "Madubuko", "Pomp", "Sauerland", "Weiss", "Wickert", "Hammes", "Deerberg", "Scheytt", "Emard", "Arndt", "Bader", "Hommel", "Moll", "Prediger", "Schuldt", "Muller", "Hengmith", "Hodea", "Karsten", "Kisabaka", "Kofferschlager", "Kunz", "Sievers", "Wassilew", "Weidlich", "Weiler", "Douglas", "Hills", "Monahan", "Senger", "Gutowicz", "Haverney", "Heinig", "Kreb", "Nabow", "Pohle", "Herzog", "Jenkins", "MacGyver", "Shields", "Yost", "James", "Bieler", "Bringmann", "Fenske", "Goy", "Beier", "Buckridge", "Hoppe", "Jaskolski", "Stroman", "Kampf", "Mebold", "Melzer", "Ritschel", "Sinnhuber", "Wehner", "Gerhardt", "Grothaus", "Hohn", "Malkus", "Molzan", "Pogorzelski", "Salow", "Schaffarzik", "Schlachter", "Strieder", "Boyle", "Luettgen", "Pacocha", "Schimmel", "Shanahan", "Ullrich", "Alexander", "Anggreny", "Eggenmueller", "Elbe", "Erwes", "Otto", "Botsford", "Lesch", "Schowalter", "Smyth", "Buschbaum", "Fassbender", "Klopsch", "Streese", "Goyette", "Busemann", "Gerdel", "Jasinski", "Kinzy", "Lippe", "Ranz", "Zahn", "Mccullough", "Bloch", "Bohge", "Galander", "Roecker", "Spelmeyer", "Steinkamp", "Tidow", "Kassulke", "Keeling", "Fleischer", "Frisch", "Giehl", "Hasler", "Koj", "Kumbernuss", "Pfersich", "Herman", "Hyatt", "Dengler", "Kirch", "Maak", "Marahrens", "Naubert", "Ryjikh", "Stelkens", "Wenk", "Zeyhle", "Kunde", "Alexa", "Geiger", "Heist", "Hold", "Marxen", "Schlechtweg", "Corkery", "Doyle", "Maggio", "Wolff", "Ernst", "Gehrig", "Grams", "Knetsch", "Minah", "Obermaier", "Gaylord", "Walter", "Wilkinson", "Dippl", "Eplinius", "Geyer", "Hartz", "Hesse", "Lichtl", "Nagel", "Schmuhl", "Viellehner", "Bechtelar", "Bruen", "Lebsack", "Carlowitz", "Edorh", "Koenig", "Laack", "Marschek", "Paschke", "Weimer", "Hansen", "Konopelski", "Felke", "Lindner", "Restle", "Riermeier", "Simon", "Toy", "Davies", "Biba", "Ewald", "Gradzki", "Hadfield", "Jakobs", "Madetzky", "Marten", "Schwanitz", "Sischka", "Casper", "Denesik", "Schuppe", "Koster", "Cole", "O'neill", "Blum", "Dyett", "Milde", "Platzer", "Dickinson", "Friesen", "Robertson", "Bethke", "Bruckmann", "Erdmann", "Fuchs", "Gauder", "Herms", "Hinrichs", "Kron", "Tafelmeier", "Wezel", "Feeney", "Hudson", "Mills", "Ondricka", "Walker", "Allgeyer", "Brunner", "Burmeister", "Dutkiewicz", "Hardy", "Hertel", "Kleiber", "Knies", "Connelly", "Kilback", "Littel", "Nienow", "Osinski", "Quitzon", "Rempel", "Falter", "Linden", "Mehlhorn", "Raukuc", "Saflanis", "Stoutjesdijk", "Ankunding", "Morgan", "Deja", "Konig", "Krol", "Voelkel", "Kirlin", "Hillard", "Leitheim", "Dibbert", "Spencer", "Stamm", "Jungbluth", "Losch", "Pichlmaier", "Roos", "Schumacher", "Stolle", "Hill", "Erhardt", "Krohn", "Nastvogel", "Rumpf", "Wiegelmann", "Wilts", "McClure", "Stoltenberg", "Phillips", "Hackelbusch", "Knoblich", "Koester", "Sammert", "Schelk", "Russel", "Agostini", "Holtfreter", "Lenfers", "Stern", "Haack", "Moser", "Prosacco", "Krug", "Loska", "Schumann", "Stein", "Gleichner", "Kessler", "Murazik", "Oberbrunner", "Parker", "Brehmer", "Briesenick", "Klimczak", "Manz", "Mehl", "Meloni", "Moeller", "Spank", "Wagner", "Wessel", "Hickle", "Kovacek", "Dittmann", "Hackbusch", "Ostendarp", "Wolfarth", "Zuber", "Torphy", "Kaesmacher", "Reitze", "Spiegelburg", "Effertz", "Gislason", "Pfeffer", "Runolfsson", "Schoen", "Towne", "Richards", "Bormann", "Helmke", "Martel", "Moedl", "Rietscher", "Griem", "Klapper", "Lenk", "Roggatz", "Kutch", "Sporer", "Bichler", "Katzinski", "Leberer", "Schenk", "Strogies", "Wachenbrunner", "Labadie", "Grube", "Meier", "Rabenstein", "Rosksch", "Koch", "Borrmann", "Cors", "Danner", "Kluge", "Leiter", "Thyssen", "Weller", "Bartoletti", "Lueilwitz", "Fink", "Gotthardt", "Knabe", "Preuk", "Pusch", "Volk", "Schiller", "Alizadeh", "Bozsik", "Engelmann", "Pfeiffer", "Riekmann", "Riepl", "Schreiber", "Stube", "Volkmann", "Hauck", "Hegmann", "Martin", "Amberg", "Dressler", "Holzner", "Schupp", "Wiebe", "Walsh", "Herzenberg", "Schwatlo", "Tuch", "Cremin", "Raynor", "Axmann", "Caspers", "Kasten", "Kleininger", "Kobs", "Kubera", "Onnen", "Hirthe", "Smith", "Bosler", "Hilgendorf", "Mesloh", "Streit", "Urhig", "Daniel", "Dicki", "Schulist", "Macdonald", "Gens", "Leimbach", "Nieklauson", "Reppin", "Rupprecht", "Wallner", "Altenwerth", "Hamill", "Jacobi", "Berger", "Klink", "Kolb", "Koob", "Liebe", "Ludolf", "Ney", "Poehn", "Schwarthoff", "Welsch", "Lindgren", "Lanig", "Mikitenko", "Paesler", "Pohland", "Reumann", "Trauth", "Hand", "Mertz", "Arens", "Hiller", "John", "Flatley", "Rowe", "Biedermann", "Damaske", "Gratzky", "Lewke", "Steigauf", "Lewis", "Meissner", "Menne", "Schreck", "Schwirkschlies", "Werner", "Zimanyi", "McLaughlin", "Murphy", "Herschmann", "Deckow", "Treutel", "Dix", "Mau", "Rossberg", "Schreiner", "Schweisfurth", "Swaniawski", "Biesenbach", "Daum", "Holdt", "Kass", "Kock", "Langfeld", "Stengl", "Zaituc", "Lowe", "O'keefe", "Badane", "Behr", "Grosskopf", "Hornung", "Kleeberg", "Mai", "Riediger", "Rosenthal", "Block", "Jacobson", "Montag", "Morhelfer", "Priemer", "Schlitzer", "Theele", "Wassiluk", "Wischer", "Beahan", "Braun", "Eckhardt", "Kemper", "Marchewski", "Newton", "Ernser", "Gast", "Heinze", "Horna", "Tasche", "Wachtel", "Boyde", "Forst", "Kaczmarek", "Kneifel", "Leiteritz", "Schlangen", "Schwalm", "Semisch", "Tischmann", "Beatty", "Fay", "Gutkowski", "Mueller", "Orn", "Blassneck", "Sewald", "Treff", "Blick", "Grady", "Taylor", "Dittmer", "Friedenberg", "Gerson", "Goedicke", "Knippel", "Kramer", "Seeger", "Steffny", "Striezel", "Kihn", "Leannon", "Schaden", "Witting", "Frey", "Hirt", "Kreissig", "Kutzner", "Reinelt", "Grimes", "Hagenes", "Lehner", "Sanford", "Beckel", "Drews", "Huls", "Stief", "Kertzmann", "Morissette", "Dittmar", "Goldbeck", "Hugo", "Kelm", "Stoll", "Vandervort", "Belz", "Bornscheuer", "Fried", "Preuss", "Ruckdeschel", "Rice", "Schamberger", "Diedrich", "Hargasser", "Hessler", "Jonas", "Maisch", "Petzold", "Scheuring", "Sokolowski", "Skiles", "Bruder", "Dobbrunz", "Kohnle", "Pressler", "Bosco", "Dare", "Hermann", "Kuhic", "Morar", "Pollich", "Everts", "Leibold", "Schroder", "Welzel", "Batz", "Durgan", "Franecki", "Lang", "Watson", "Leipold", "Matthes", "Sladek", "Unger", "Unterpaintner", "Werle", "Bradtke", "Champlin", "Bogdashin", "Bremer", "Leiwesmeier", "Roth", "Sarvari", "Wanner", "Goodwin", "Howe", "Huels", "Jerde", "Benninger", "Bogenrieder", "Burkhard", "Giesa", "Harting", "Polifka", "Rockmeier", "Schlicht", "Uhrig", "Evers", "Hujo", "Kovacs", "Nussbeck", "Piesker", "Scheer", "Schilling", "Schulte", "Seelig", "Tang", "Zaczkiewicz", "Gleason", "Zemlak", "Thomson", "O'hara", "Bertenbreiter", "Best", "Cierpinski", "Bernier", "Brekke", "Hirschberg", "Vahlensieck", "Zeidler", "Zintl", "Frami", "Sawayn", "Wiegand", "Blockhaus", "Menga", "Ledner", "Rittweg", "Schuermann", "Stratmann", "Upton", "Kuschewitz", "Lakomy", "Lobinger", "Sagonas", "Schembera", "Banse", "Hartlieb", "Jahnke", "Kleinert", "Reinhardt", "Sihler", "Davis", "Brix", "Holinski", "Isekenmeier", "Krull", "Kummle", "Kuske", "Lohmann", "Nicolay", "Fadel", "Kemmer", "Bartels", "Horn", "Howard", "Jasper", "Krebs", "Scc", "Schwerdtner", "Schwuchow", "Umbach", "Umlauft", "Retzke", "Seidel", "Thriene", "Waibel", "Griffiths", "Borsch", "Flottmann", "Freimann", "Jeorga", "Lauinger", "Naumann", "Crooks", "Metz", "Terry", "Ackermann", "Bourrouag", "Gutjahr", "Hingsen", "Kiessling", "Kohle", "Lindenberg", "Sattelmaier", "Schreiter", "Howell", "Berends", "Glatting", "Jagusch", "Just", "Marl", "Neumair", "Plautz", "Rosenbauer", "Smieja", "Beer", "Blochwitz", "Kaiser", "Nowak", "Siegling", "Teichmann", "Wieser", "Zender", "Koelpin", "Schneider", "Torp", "Nguyen", "Eberhard", "Eifert", "Frantz", "Heuck", "Meyer", "Schley", "Schouren", "Siener", "Barth", "Behrenbruch", "Geisler", "Gesell", "Henkel", "Jossa", "Schult", "Verniest", "Winkler", "Zandt", "Adams", "Kling", "McCullough", "Pfannerstill", "Schuster", "Hentschke", "Hildebrand", "Holz", "Humbert", "Poser", "Strausa", "Wiese", "Kozey", "Zulauf", "Storl", "Stehr", "Wisozk", "Chapron", "Reuber", "Reinger", "Filsinger", "Grundmann", "Isak", "Philipp", "Scherz", "Schulze", "Uhlemann", "Gerhold", "Jast", "Lockman", "Berner", "Busse", "Bussmann", "Dombrowski", "Frenzel", "Frohn", "Gotz", "Grau", "Deuschle", "Ehrig", "Malek", "Richter", "Sack", "Schoberg", "Moore", "Ward", "Ehm", "Friess", "Merkel", "Porth", "Siebel", "Steinecke", "Strunz", "Bauermeister", "Dietzsch", "Faller", "Goebel", "Leschnik", "Prah", "Renner", "Doherty", "Deckert", "Krauel", "Ne", "Seigel", "Seiler", "Parisian", "Ratke", "Breuer", "Co", "Koubaa", "Itt", "Kruger", "Lipske", "Pletsch", "Tschiers", "Bogisich", "Gerlach", "Danneberg", "Harter", "Ritter", "Hetzler", "Lichtenhagen", "Mattern", "Peter", "Reinke", "Sprenger", "Venghaus", "Walton", "O'Keefe", "Predovic", "Quigley", "Bork", "Engel", "Harnapp", "Hofmann", "Lack", "Lauckner", "Stolz", "Wartenberg", "King", "Willms", "Scott", "Aryee", "Bremser", "Klabuhn", "Lichtenfeld", "Schuhaj", "Zekl", "Daimer", "Jamrozy", "Kahles", "Koha", "Kuprion", "Kutscherauer" };
        private readonly List<String> _maleFirstNames = new List<String> { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas", "Christopher", "Daniel", "Paul", "Mark", "Donald", "George", "Kenneth", "Steven", "Edward", "Brian", "Ronald", "Anthony", "Kevin", "Jason", "Matthew", "Gary", "Timothy", "Jose", "Larry", "Jeffrey", "Frank", "Scott", "Eric", "Stephen", "Andrew", "Raymond", "Gregory", "Joshua", "Jerry", "Dennis", "Walter", "Patrick", "Peter", "Harold", "Douglas", "Henry", "Carl", "Arthur", "Ryan", "Roger", "Joe", "Juan", "Jack", "Albert", "Jonathan", "Justin", "Terry", "Gerald", "Keith", "Samuel", "Willie", "Ralph", "Lawrence", "Nicholas", "Roy", "Benjamin", "Bruce", "Brandon", "Adam", "Harry", "Fred", "Wayne", "Billy", "Steve", "Louis", "Jeremy", "Aaron", "Randy", "Howard", "Eugene", "Carlos", "Russell", "Bobby", "Victor", "Martin", "Ernest", "Phillip", "Todd", "Jesse", "Craig", "Alan", "Shawn", "Clarence", "Sean", "Philip", "Chris", "Johnny", "Earl", "Jimmy", "Antonio", "Danny", "Bryan", "Tony", "Luis", "Mike", "Stanley", "Leonard", "Nathan", "Dale", "Manuel", "Rodney", "Curtis", "Norman", "Allen", "Marvin", "Vincent", "Glenn", "Jeffery", "Travis", "Jeff", "Chad", "Jacob", "Lee", "Melvin", "Alfred", "Kyle", "Francis", "Bradley", "Jesus", "Herbert", "Frederick", "Ray", "Joel", "Edwin", "Don", "Eddie", "Ricky", "Troy", "Randall", "Barry", "Alexander", "Bernard", "Mario", "Leroy", "Francisco", "Marcus", "Micheal", "Theodore", "Clifford", "Miguel", "Oscar", "Jay", "Jim", "Tom", "Calvin", "Alex", "Jon", "Ronnie", "Bill", "Lloyd", "Tommy", "Leon", "Derek", "Warren", "Darrell", "Jerome", "Floyd", "Leo", "Alvin", "Tim", "Wesley", "Gordon", "Dean", "Greg", "Jorge", "Dustin", "Pedro", "Derrick", "Dan", "Lewis", "Zachary", "Corey", "Herman", "Maurice", "Vernon", "Roberto", "Clyde", "Glen", "Hector", "Shane", "Ricardo", "Sam", "Rick", "Lester", "Brent", "Ramon", "Charlie", "Tyler", "Gilbert", "Gene", "Marc", "Reginald", "Ruben", "Brett", "Angel", "Nathaniel", "Rafael", "Leslie", "Edgar", "Milton", "Raul", "Ben", "Chester", "Cecil", "Duane", "Franklin", "Andre", "Elmer", "Brad", "Gabriel", "Ron", "Mitchell", "Roland", "Arnold", "Harvey", "Jared", "Adrian", "Karl", "Cory", "Claude", "Erik", "Darryl", "Jamie", "Neil", "Jessie", "Christian", "Javier", "Fernando", "Clinton", "Ted", "Mathew", "Tyrone", "Darren", "Lonnie", "Lance", "Cody", "Julio", "Kelly", "Kurt", "Allan", "Nelson", "Guy", "Clayton", "Hugh", "Max", "Dwayne", "Dwight", "Armando", "Felix", "Jimmie", "Everett", "Jordan", "Ian", "Wallace", "Ken", "Bob", "Jaime", "Casey", "Alfredo", "Alberto", "Dave", "Ivan", "Johnnie", "Sidney", "Byron", "Julian", "Isaac", "Morris", "Clifton", "Willard", "Daryl", "Ross", "Virgil", "Andy", "Marshall", "Salvador", "Perry", "Kirk", "Sergio", "Marion", "Tracy", "Seth", "Kent", "Terrance", "Rene", "Eduardo", "Terrence", "Enrique", "Freddie", "Wade", "Austin", "Stuart", "Fredrick", "Arturo", "Alejandro", "Jackie", "Joey", "Nick", "Luther", "Wendell", "Jeremiah", "Evan", "Julius", "Dana", "Donnie", "Otis", "Shannon", "Trevor", "Oliver", "Luke", "Homer", "Gerard", "Doug", "Kenny", "Hubert", "Angelo", "Shaun", "Lyle", "Matt", "Lynn", "Alfonso", "Orlando", "Rex", "Carlton", "Ernesto", "Cameron", "Neal", "Pablo", "Lorenzo", "Omar", "Wilbur", "Blake", "Grant", "Horace", "Roderick", "Kerry", "Abraham", "Willis", "Rickey", "Jean", "Ira", "Andres", "Cesar", "Johnathan", "Malcolm", "Rudolph", "Damon", "Kelvin", "Rudy", "Preston", "Alton", "Archie", "Marco", "Wm", "Pete", "Randolph", "Garry", "Geoffrey", "Jonathon", "Felipe", "Bennie", "Gerardo", "Ed", "Dominic", "Robin", "Loren", "Delbert", "Colin", "Guillermo", "Earnest", "Lucas", "Benny", "Noel", "Spencer", "Rodolfo", "Myron", "Edmund", "Garrett", "Salvatore", "Cedric", "Lowell", "Gregg", "Sherman", "Wilson", "Devin", "Sylvester", "Kim", "Roosevelt", "Israel", "Jermaine", "Forrest", "Wilbert", "Leland", "Simon", "Guadalupe", "Clark", "Irving", "Carroll", "Bryant", "Owen", "Rufus", "Woodrow", "Sammy", "Kristopher", "Mack", "Levi", "Marcos", "Gustavo", "Jake", "Lionel", "Marty", "Taylor", "Ellis", "Dallas", "Gilberto", "Clint", "Nicolas", "Laurence", "Ismael", "Orville", "Drew", "Jody", "Ervin", "Dewey", "Al", "Wilfred", "Josh", "Hugo", "Ignacio", "Caleb", "Tomas", "Sheldon", "Erick", "Frankie", "Stewart", "Doyle", "Darrel", "Rogelio", "Terence", "Santiago", "Alonzo", "Elias", "Bert", "Elbert", "Ramiro", "Conrad", "Pat", "Noah", "Grady", "Phil", "Cornelius", "Lamar", "Rolando", "Clay", "Percy", "Dexter", "Bradford", "Merle", "Darin", "Amos", "Terrell", "Moses", "Irvin", "Saul", "Roman", "Darnell", "Randal", "Tommie", "Timmy", "Darrin", "Winston", "Brendan", "Toby", "Van", "Abel", "Dominick", "Boyd", "Courtney", "Jan", "Emilio", "Elijah", "Cary", "Domingo", "Santos", "Aubrey", "Emmett", "Marlon", "Emanuel", "Jerald", "Edmond" };
        private readonly List<String> _occupations = new List<String> { "Account Collector", "Accounting Specialist", "Administrative Assistant", "Advertising Account Executive", "Agricultural Engineer", "Agricultural Equipment Operator", "Air Crew Member", "Air Crew Officer", "Air Traffic Controller", "Aircraft Assembler", "Animal Trainer", "Athletic Coach", "Athletic Director", "Automobile Mechanic", "Baker (Commercial)", "Book Editor", "Budget Accountant", "Carpet Installer", "Chief Financial Officer", "Computer Programmer", "Computer Science Professor", "Computer Security Specialist", "Computer Software Engineer", "Computer Systems Engineer", "Delivery Driver", "Dentist (MD)", "Dry Wall Installer", "Economics Professor", "Electronics Technician", "Elementary School Administrator", "Elementary School Teacher", "Elevator Mechanic", "Emergency Medical Technician", "Employee Benefits Analyst", "Farm Equipment Mechanic", "Flight Engineer", "Floral Designer", "Furniture Designer", "Hotel Manager", "Insurance Claims Adjuster", "Kindergarten Teacher", "Mail Machine Operator", "Maintenance Supervisor", "Metal Fabricator", "Middle School Administrator", "Middle School Teacher", "Music Director", "Music Teacher", "Preschool Teacher", "Private Nurse", "Purchasing Manager", "Restaurant Manager", "Retail Buyer", "Sales Manager", "School Nurse", "Surgeon (MD)", "Tax Accountant", "Tax Preparer", "Taxi Driver ", "Truck Driver", "Veterinarian (VMD)", "Veterinary Assistant" };
        readonly Random _random;
        readonly StringBuilder _sb = new StringBuilder();
        readonly List<String> _states = new List<String> { "AA", "AE", "AK", "AL", "AP", "AR", "AS", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "FM", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MH", "MI", "MN", "MO", "MP", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "PW", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY" };
        readonly List<String> _streets = new List<String> { "Commerce Drive", "Pinelawn Road", "Echevarriarza", "Hammersmith Road", "B,Simeonovsko Shosse Bul", "Pennsylvania Ave", "Wall Street", "Broadway", "Lombard Street", "Michigan Avenue", "Beartooth Pass", "Million Dollar Highway" };
        readonly List<String> _urls = new List<String> { "http://microsoft.com", "http://oceanware.wordpress.com", "http://agsmith.wordpress.com/", "http://www.beacosta.com/blog/", "http://wekempf.spaces.live.com/default.aspx", "http://x-coders.com/blogs/sneaky/default.aspx", "http://blogs.ugidotnet.org/corrado/Default.aspx", "http://sachabarber.net/", "http://weblogs.asp.net/scottgu/" };
        readonly Int32 _wordLowerBound = 0;
        readonly String[] _words = { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim", "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore", "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata", "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };
        readonly Int32 _wordUpperBound;
        IList<CompanyItem> _companyItems;
        Int32 _currentCityIndex;
        Int32 _currentCompanyItemIndex;
        Int32 _currentCountryIndex;
        Int32 _currentEmailProviderIndex;
        Int32 _currentFemaleFirstNameIndex;
        Int32 _currentLastNameIndex;
        Int32 _currentMaleFirstNameIndex;
        Int32 _currentOccupationIndex;
        Int32 _currentPersonItemIndex = -1;
        Int32 _currentStateIndex;
        Int32 _currentStreetIndex;
        Int32 _currentUrlIndex;
        Int32 _incrementValue = 1;
        IList<PersonItem> _personItems;
        Int32 _seedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGenerator"/> class.
        /// </summary>
        public DataGenerator() {
            var randomBytes = new Byte[4];
            var random = new Random();
            random.NextBytes(randomBytes);
            Int32 seed = (randomBytes[0] & 0X7F) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3];
            _random = new Random(seed);
            SeedSequentialInteger(1000, 1);
            _wordUpperBound = _words.GetUpperBound(0);
        }

        /// <summary>
        /// Gets the address item.
        /// </summary>
        /// <returns><see cref="AddressItem"/></returns>
        public AddressItem GetAddressItem() {
            return new AddressItem(GetPersonItem());
        }

        /// <summary>
        /// Gets a birthday for an adutl.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime GetAdultBirthday() {
            var year = DateTime.Now.Year;
            return GetDate(new DateTime(GetInteger(year - 50, year - 30), GetInteger(1, 12), GetInteger(1, 28)), new DateTime(GetInteger(year - 29, year - 20), GetInteger(1, 12), GetInteger(1, 28)));
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <returns>Boolean that is randomly set to <c>true</c> or <c>false</c>.</returns>
        public Boolean GetBoolean() {
            return _random.Next(0, 49) % 2 == 0;
        }

        /// <summary>
        /// Gets the Byte.
        /// </summary>
        /// <returns>Byte.</returns>
        public Byte GetByte() {
            return Convert.ToByte(_random.Next(0, 255));
        }

        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <returns>Char.</returns>
        public Char GetChar() {
            return Convert.ToChar(_random.Next(0, UInt16.MaxValue));
        }

        /// <summary>
        /// Gets a birthday for a child.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime GetChildBirthday() {
            var year = DateTime.Now.Year;
            return GetDate(new DateTime(GetInteger(year - 15, year - 5), GetInteger(1, 12), GetInteger(1, 28)), new DateTime(GetInteger(year - 4, year - 0), GetInteger(1, 12), GetInteger(1, 28)));
        }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <returns>String.</returns>
        public String GetCity() {
            _currentCityIndex += 1;
            if (_currentCityIndex > _cities.Count - 1) {
                _currentCityIndex = 0;
            }
            return _cities[_currentCityIndex];
        }

        /// <summary>
        /// Gets the next company item.
        /// </summary>
        /// <returns>CompanyItem.</returns>
        public CompanyItem GetCompanyItem() {
            if (_companyItems == null) {
                GetCompanyItems();
            }
            _currentCompanyItemIndex++;
            if (_currentCompanyItemIndex > _companyItems.Count - 1) {
                _currentCompanyItemIndex = 0;
            }
            return _companyItems[_currentCompanyItemIndex];
        }

        /// <summary>
        /// Gets the company items.
        /// </summary>
        /// <returns>IList&lt;CompanyItem&gt;.</returns>
        public IList<CompanyItem> GetCompanyItems() {
            var json = GetEmbeddedResource("companies.json", Assembly.GetExecutingAssembly());
            var list = JsonConvert.DeserializeObject<List<CompanyItem>>(json);
            _companyItems = list.ToList();
            return _companyItems;
        }

        /// <summary>
        /// Gets a randomly selected company name.
        /// </summary>
        /// <returns>String with a randomly selected company name.</returns>
        public String GetCompanyName() {
            return GetCompanyItem().CompanyName;
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <returns>String.</returns>
        public String GetCountry() {
            _currentCountryIndex += 1;
            if (_currentCountryIndex > _countries.Count - 1) {
                _currentCountryIndex = 0;
            }
            return _countries[_currentCountryIndex];
        }

        /// <summary>Gets the date that is between minValue and maxValue.</summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>DateTime that is between minValue and maxValue.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">minValue - Is greater than maxValue</exception>
        public DateTime GetDate(DateTime minValue, DateTime maxValue) {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException(nameof(minValue), Strings.IsGreaterThanMaxValue);
            }
            if (minValue == maxValue) {
                return minValue;
            }
            TimeSpan ts = maxValue - minValue;

            Int32 dateDiff = Math.Abs(ts.TotalDays) > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(Math.Abs(ts.TotalDays));

            return minValue.AddDays(GetInteger(0, dateDiff));
        }

        /// <summary>
        /// Gets the decimal that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Decimal that is between minValue and maxValue.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">minValue - Is greater than maxValue</exception>
        public Decimal GetDecimal(Int32 minValue, Int32 maxValue) {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException(nameof(minValue), Strings.IsGreaterThanMaxValue);
            }
            if (minValue == maxValue) {
                return Convert.ToDecimal(minValue);
            }
            return Convert.ToDecimal(_random.Next(minValue, maxValue) + _random.NextDouble());
        }

        /// <summary>
        /// Gets the Double that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Double that is between minValue and maxValue.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">minValue - Is greater than maxValue</exception>
        public Double GetDouble(Int32 minValue, Int32 maxValue) {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException(nameof(minValue), Strings.IsGreaterThanMaxValue);
            }
            if (minValue == maxValue) {
                return Convert.ToDouble(minValue);
            }
            return _random.Next(minValue, maxValue) + _random.NextDouble();
        }

        /// <summary>
        /// Gets the randomly created email address.
        /// </summary>
        /// <returns>String with a randomly created email address.</returns>
        public String GetEmail() {
            return $"{GetString(7, StringCase.Lower, RemoveSpaces.Yes)}@{GetEmailProvider()}.com";
        }

        /// <summary>
        /// Gets the next email provider.
        /// </summary>
        /// <returns>System.String.</returns>
        public String GetEmailProvider() {
            _currentEmailProviderIndex += 1;
            if (_currentEmailProviderIndex > _emailProviders.Count - 1) {
                _currentEmailProviderIndex = 0;
            }
            return _emailProviders[_currentEmailProviderIndex];
        }

        /// <summary>
        /// Gets  the next female first name.
        /// </summary>
        /// <returns>System.String.</returns>
        public String GetFemaleFirstName() {
            _currentFemaleFirstNameIndex += 1;
            if (_currentFemaleFirstNameIndex > _femaleFirstNames.Count - 1) {
                _currentFemaleFirstNameIndex = 0;
            }
            return _femaleFirstNames[_currentFemaleFirstNameIndex];
        }

        /// <summary>
        /// Gets a randomly selected female full name
        /// </summary>
        /// <returns>String with a randomly selected full name.</returns>
        public String GetFemaleFullName() {
            return $"{GetFemaleFirstName()} {GetLastName()}";
        }

        /// <summary>
        /// Gets an integer that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Integer that is between minValue and maxValue.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">minValue - Is greater than maxValue</exception>
        public Int32 GetInteger(Int32 minValue, Int32 maxValue) {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException(nameof(minValue), Strings.IsGreaterThanMaxValue);
            }
            if (minValue == maxValue) {
                return minValue;
            }
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Gets a randomly selected last name.
        /// </summary>
        /// <returns>String with a randomly selected last name.</returns>
        public String GetLastName() {
            _currentLastNameIndex += 1;
            if (_currentLastNameIndex > _lastNames.Count - 1) {
                _currentLastNameIndex = 0;
            }
            return _lastNames[_currentLastNameIndex];
        }

        /// <summary>
        /// Gets  the next male first name.
        /// </summary>
        /// <returns>System.String.</returns>
        public String GetMaleFirstName() {
            _currentMaleFirstNameIndex += 1;
            if (_currentMaleFirstNameIndex > _maleFirstNames.Count - 1) {
                _currentMaleFirstNameIndex = 0;
            }
            return _maleFirstNames[_currentMaleFirstNameIndex];
        }

        /// <summary>
        /// Gets a randomly selected male full name
        /// </summary>
        /// <returns>String with a randomly selected full name.</returns>
        public String GetMaleFullName() {
            return $"{GetMaleFirstName()} {GetLastName()}";
        }

        /// <summary>
        /// Gets the next occupation.
        /// </summary>
        /// <returns>String.</returns>
        public String GetOccupation() {
            _currentOccupationIndex += 1;
            if (_currentOccupationIndex > _occupations.Count - 1) {
                _currentOccupationIndex = 0;
            }
            return _occupations[_currentOccupationIndex];
        }

        /// <summary>
        /// Gets a <seealso cref="PersonItem"./>
        /// </summary>
        /// <returns><see cref="PersonItem"/>.</returns>
        public PersonItem GetPersonItem() {
            if (_personItems == null) {
                GetPersonItems();
            }
            _currentPersonItemIndex++;
            if (_currentPersonItemIndex > _personItems.Count - 1) {
                _currentPersonItemIndex = 0;
            }
            return _personItems[_currentPersonItemIndex];
        }

        /// <summary>
        /// Gets all <seealso cref="PersonItem" />.
        /// </summary>
        /// <returns>IList&lt;PersonItem&gt;.</returns>
        public IList<PersonItem> GetPersonItems() {
            var json = GetEmbeddedResource("sampleData.json", Assembly.GetExecutingAssembly());
            var list = JsonConvert.DeserializeObject<List<PersonItem>>(json);
            _personItems = list.ToList();
            return _personItems;
        }

        /// <summary>
        /// Gets a randomly generated formatted phone number (###) ###-####
        /// </summary>
        /// <returns>String with a randomly generated phone number.</returns>
        public String GetPhoneNumber() {
            return $"({GetInteger(100, 999)}) {GetInteger(100, 999)}-{GetInteger(1000, 9999)}";
        }

        /// <summary>
        /// Gets a randomly generated phone number with only the digits
        /// </summary>
        /// <returns>String with a randomly generated phone number.</returns>
        public String GetPhoneNumberDigits() {
            return $"{GetInteger(100, 999)}{GetInteger(100, 999)}{GetInteger(1000, 9999)}";
        }

        /// <summary>
        /// Gets the s Byte.
        /// </summary>
        /// <returns>SByte.</returns>
        public SByte GetSByte() {
            return Convert.ToSByte(_random.Next(-128, 127));
        }

        /// <summary>
        /// Gets the next sequential integer. Each time this property is accessed the sequential integer is incremented.
        /// </summary>
        /// <returns>Integer representing the next sequential integer.</returns>
        public Int32 GetSequentialInteger() {
            _seedValue += _incrementValue;
            return _seedValue;
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>Single.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">minValue - Is greater than maxValue</exception>
        public Single GetSingle(Int32 minValue, Int32 maxValue) {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException(nameof(minValue), Strings.IsGreaterThanMaxValue);
            }
            if (minValue == maxValue) {
                return Convert.ToSingle(minValue);
            }
            return Convert.ToSingle(_random.Next(minValue, maxValue) + _random.NextDouble());
        }

        /// <summary>
        /// Gets a randomly generated social security number.
        /// </summary>
        /// <returns>String with a randomly generated social security number.</returns>
        public String GetSSN() {
            return $"{GetInteger(100, 999)}-{GetInteger(10, 99)}-{GetInteger(1000, 9999)}";
        }

        /// <summary>
        /// Gets a randomly selected state abbreviation.
        /// </summary>
        /// <returns>String with a randomly selected state abbreviation.</returns>
        public String GetStateAbbreviation() {
            _currentStateIndex += 1;
            if (_currentStateIndex > _states.Count - 1) {
                _currentStateIndex = 0;
            }
            return _states[_currentStateIndex];
        }

        /// <summary>
        /// Gets the street.
        /// </summary>
        /// <returns>String.</returns>
        public String GetStreet() {
            _currentStreetIndex += 1;
            if (_currentStreetIndex > _streets.Count - 1) {
                _currentStreetIndex = 0;
            }
            return _streets[_currentStreetIndex];
        }

        /// <summary>
        /// Gets a String of random lorin epsum having a length equal to or less than the max length argument.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned String.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument.</returns>
        public String GetString(Int32 maxLength) {
            return GetString(maxLength, StringCase.None, RemoveSpaces.No);
        }

        /// <summary>
        /// Gets a String of random lorin epsum having a length equal to or less than the max length argument along with the specified String case applied.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned String.</param>
        /// <param name="stringCase">The String case rule to apply.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument along with the specified String case applied.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value stringCase is not defined.</exception>
        public String GetString(Int32 maxLength, StringCase stringCase) {
            return GetString(maxLength, stringCase, RemoveSpaces.No);
        }

        /// <summary>Gets a String of random lorin epsum having a length equal to or less than the max length argument along with the specified String case applied.</summary>
        /// <param name="maxLength">Maximum length of the returned String.</param>
        /// <param name="stringCase">The String case rule to apply.</param>
        /// <param name="removeSpaces">if set to <c>Yes</c> all spaces will be removed.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument along with the specified String case applied.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value stringCase is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value removeSpaces is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value stringCase is not defined.</exception>
        public String GetString(Int32 maxLength, StringCase stringCase, RemoveSpaces removeSpaces) {
            if (!Enum.IsDefined(typeof(StringCase), stringCase)) {
                throw new InvalidEnumArgumentException(nameof(stringCase), (Int32)stringCase, typeof(StringCase));
            }
            if (!Enum.IsDefined(typeof(RemoveSpaces), removeSpaces)) {
                throw new InvalidEnumArgumentException(nameof(removeSpaces), (Int32)removeSpaces, typeof(RemoveSpaces));
            }

            _sb.Clear();
            _sb.Length = 0;
            while (_sb.Length < maxLength) {
                _sb.Append(_words[_random.Next(_wordLowerBound, _wordUpperBound)]);
                if (removeSpaces == RemoveSpaces.No) {
                    _sb.Append(_stringWhiteSpace);
                }
            }
            _sb.Length = maxLength;

            switch (stringCase) {
                case StringCase.Lower:
                    return _sb.ToString().ToLower();

                case StringCase.None:
                    return _sb.ToString();

                case StringCase.Upper:
                    return _sb.ToString().ToUpper();

                default:
                    throw new InvalidEnumValueException(typeof(StringCase), stringCase);
            }
        }

        /// <summary>
        /// Gets a randomly selected Url.
        /// </summary>
        /// <returns>String with a randomly selected Url.</returns>
        public String GetUrl() {
            _currentUrlIndex += 1;
            if (_currentUrlIndex > _urls.Count - 1) {
                _currentUrlIndex = 0;
            }
            return _urls[_currentUrlIndex];
        }

        /// <summary>
        /// Gets a randomly generated zip code.
        /// </summary>
        /// <returns>String with a randomly generated zip code.</returns>
        public String GetZipCode() {
            return GetInteger(1000, 99999).ToString("D5");
        }

        /// <summary>
        /// Gets a randomly generated zip code with plus 4.
        /// </summary>
        /// <returns>String with a randomly generated zip code with plus 4.</returns>
        public String GetZipCodeWithPlusFour() {
            return $"{GetInteger(1000, 99999).ToString("D5")}-{GetInteger(100, 9999).ToString("D4")}";
        }

        /// <summary>
        /// Seeds the sequential integer.
        /// </summary>
        /// <param name="seedValue">The seed value.</param>
        /// <param name="incrementValue">The increment value.</param>
        public void SeedSequentialInteger(Int32 seedValue, Int32 incrementValue) {
            _seedValue = seedValue;
            _incrementValue = incrementValue;
        }

        String FormatResourceName(Assembly assembly, String resourceName) {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }

        String GetEmbeddedResource(String resourceName, Assembly assembly) {
            resourceName = FormatResourceName(assembly, resourceName);
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName)) {
                if (resourceStream == null) {
                    return null;
                }

                using (StreamReader reader = new StreamReader(resourceStream)) {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
