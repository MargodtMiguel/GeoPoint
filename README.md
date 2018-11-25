# GeoPoint

Game: De gebruiker krijgt de naam van een land / stad te zien op het scherm en een kaart. Hij moet nu raden waar op de kaart het land / de stad ligt. Telkens hij het juist heeft krijg hij en punt. Bij een foutief antwoord stopt het spel en krijgt de gebruiker het juiste antwoord te zien. Zo kan hij bijleren en zijn persoonlijke score verslaan.

Het is mogelijk om vrienden toe te voegen om zo enkel de top scores van je vrienden te zien bij 'leaderboards' (in plaats van alle topscores). 

Realtime: Vriendschapsverzoeken komen realtime op het scherm

## Backend vereisten

| Vereisten                    | Info                            			| Check |    
| -----------------------------|--------------------------------------------------------|:-----:|
| Backend API                  | ASP.NET Core MVC API            			|   V   |
| De database                  | MongoDB                         			|   V   |
| Realtime                     | Bij het versturen van een vriendschapsverzoek komt dit<br>realtime op het scherm van de andere persoon.                                			|   V   |
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
| Testing                      | Er zijn 4 unit tests aanwezig, gebruik gemaakt van Karma|   V   |
| Styling                      | SCSS                            			|   V   |
| PWA                          | Heeft een correcte PWA set-up   			|   V   |
| Multi-language               | i18n gebruikt voor het toevoegen van meerdere talen	|   V   |
| Error logging                | Error logging a.d.h.V sentry.io (setup in main.js)	|   V   |
| Development setup            | Code climat gebruikt -> duplicate code verwijdert.	|   V   |
| Webpack optimisation         |                                 			|       |
| Eigen inbreng                | SVG map pan en zoom<br>Wachtwoord sterkte meter<br>Slideout-panel bij 'Add friend'<br>Notification bij het ontvangen van een vriendschapsverzoek|   V   |
