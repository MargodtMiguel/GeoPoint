# GeoPoint

Game: De gebruiker krijgt de naam van een land / stad te zien op het scherm en een kaart. Hij moet nu raden waar op de kaart het land / de stad ligt. Telkens hij het juist heeft krijg hij en punt. Bij een foutief antwoord stopt het spel en krijgt de gebruiker het juiste antwoord te zien. Zo kan hij bijleren en zijn persoonlijke score verslaan.

De gebruiker kan ook badges verdienen voor op zijn profiel. Voorbeeld: Badge bij het voltooien van de map "Europa".

Het is mogelijk om vrienden toe te voegen om zo hun score's te zien. 

Realtime: Scoreborden (world + friends) + vriendschapsverzoeken

## Backend vereisten

| Vereisten                    | Info                            			| Check |    
| -----------------------------|--------------------------------------------------------|:-----:|
| Backend API                  | ASP.NET Core MVC API            			|   V   |
| De database                  | MongoDB                         			|   V   |
| Realtime                     |                                 			|       |
| Autorisatie en authenticatie | JWT Token authentication        			|   V   |
| Kwetsbaarheid                | Cors http://localhost:8080      			|   V   |
| API Docs                     | Swagger                         			|   V   |
| Framework                    | Seeder ingesteld                			|   V   |
| Foutcontrole                 | Try/Catch on controllers with Logging            	|   V   |
| Source controle              | Github                          			|   V   |
| Deployment                   |                                 			|       |
| Eigen inbgeng                |                                 			|       |

## Frontend vereisten
 
| Vereisten                    | Info                            			| Check |  
| -----------------------------|--------------------------------------------------------|:-----:|
| Framework                    | Gebruik gemaakt van vue.js      			|   V   |
| Testing                      |                                 			|       |
| Styling                      | SCSS                            			|   V   |
| PWA                          | Heeft een correcte PWA set-up   			|   V   |
| Multi-language               |                                 			|       |
| Error logging                | Error logging a.d.h.V sentry.io (setup in main.js)	|   V   |
| Development setup            |                                			|       |
| Webpack optimisation         |                                 			|       |
| Eigen inbreng                | SVG map pan en zoom + password strength          	|   V   |
