# SSiApi
Folderen "DatabaseFiles" har sql filerne til at få lavet de tables jeg brugte, og ellers er SSIApi projektet api'en, og WebApplicationSSI webapplikationen.
Husk at rette appsettings.json i SSIApi til med den rigtige connectionstring, hvis i prøver det i aktion.

I applikationen kan man logge ind med et brugernavn og kodeord, som man kan lave ved at kalde /api/users/ med en POST, 
og så sende fornavn, efternavn, brugernavn og kodeord. Formatet er JSON, eks:
{
	"firstname" : "Kris",
	"lastname" : "Jensen",
    "username": "kris",
    "password": "1234"
}

For at få lov til at lave requests, skal man bruge en JWT token, som man får ved at kalde endpointet /api/users/authenticate med brugernavn og kodeord, 
hvorefter man så får sendt noget JSON tilbage med en bearer token(JWT), som man så skal bruge i postman(eller hvad i nu bruger), når man laver requests.

Der er desværre ikke noget UI til andet end at se indholdet af de forskellige tabeller, da det lå lidt udover den tid jeg havde :)

Liste over endpoints:
/api/users
/api/users/authenticate

/api/articles

/api/stockitems

/api/locations
