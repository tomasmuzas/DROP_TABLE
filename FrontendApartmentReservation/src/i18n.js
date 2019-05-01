import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";

i18n.use(LanguageDetector).init({
  // we init with resources
  resources: {
    en: {
      translations: {
        Text: "translation",
        "Apartments": "Apartments",
        "Authentication": "Authentication",
        "Trips": "Trips",
        "Users": "Users",
        "SignUp": "Sign up!",
        "Name": "Name",
        "Surname": "Surname",
        "Email": "Email",
        "Password": "Password",
        "Office": "Office",
        "SignUpError": "Error:",
        "PasswordError": "Password must contain one uppercase one lowercase, special symbol and be 5-15 chars length",
        "CreateTrip": "Create a trip",
        "SelectEmployees": "Select employees",
        "SelectOffice": "Select DB office",
        "Cost": "Cost",
        "DepartureDate": "Departure time",
        "ReturnDate": "Return date",
        "CarRequired": "Is car required?",
        "ApartmentRequired": "Are apartments required?",
        "FlightRequired": "Is flight required?",
        "FinishCreation": "Finish",
        "FlightNumber": "Flight Number",
        "FlightTime": "Flight Time",
        "AirportAddress": "Airport Address",
        "FlightCompany": "Flight Company",
        "SaveFlightInfo": "Save flight info",
        "SaveCarInfo": "Save car info",
        "SaveApartmentsInfo": "Save apartments info",
        "CarNumber": "Car Number",
        "ApartmentsAddres": "Apartments Address",
        "FlightInfoUpdate": "Flight info updated",
        "CarInfoUpdate": "Car info updated",
        "ApartmentsInfoUpdated":"Apartments info updated",
        "DestinationOffice": "Destination Office",
      }
    },
    lt: {
      translations: {
        Text: "translation",
        "Apartments": "Apartamentai",
        "Authentication": "Autentikacija",
        "Trips": "Keliones",
        "Users": "Naudotojai",
        "SignUp": "Užsiregistruoti",
        "Name": "Vardas",
        "Surname": "Pavardė",
        "Email": "Elektroninis paštas",
        "Password": "Slaptažodis",
        "Office": "Ofisas",
        "SignUpError": "Klaida registruojantis:",
        "PasswordError": "Slaptažodis turi turėti vieną didžiąją vieną ir mažąją raides, specialų simbolį ir būti 5-15 simbolių ilgio",
        "CreateTrip": "Sukurti kelionę",
        "SelectEmployees": "Pasirinkite darbuotojus",
        "SelectOffice": "Pasirinkite DB offisą",
        "Cost": "Kaina",
        "DepartureDate": "Išvykimo data",
        "ReturnDate": "Grįžimo data",
        "CarRequired": "Ar mašina reikalinga?",
        "ApartmentRequired": "Ar apartamentai reikalingi?",
        "FlightRequired": "Ar skrydžiai reikalingi?",
        "FinishCreation": "Užbaigti",
        "FlightNumber": "Skrydžio numeris",
        "FlightTime": "Skrydžio laikas",
        "AirportAddress": "Oro uosto adresas",
        "FlightCompany": "Skrydžių kompanija",
        "SaveFlightInfo": "Saugoti skrydžio info",
        "SaveCarInfo": "Saugoti mašinos info",
        "SaveApartmentsInfo": "Saugoti apartamentų info",
        "CarNumber": "Mašinos numeris",
        "ApartmentsAddres": "Apartamentų adresas",
        "FlightInfoUpdate": "Skrydžio informacija išsaugota",
        "CarInfoUpdate": "Mašinos informacija išsaugota",
        "ApartmentsInfoUpdated":"Apartamentų informacija išsaugota",
        "DestinationOffice": "Išvykimo ofisas",
      }
    }
  },
  fallbackLng: "en",
  debug: true,

  // have a common namespace used around the full app
  ns: ["translations"],
  defaultNS: "translations",

  keySeparator: false, // we use content as keys

  interpolation: {
    escapeValue: false, // not needed for react!!
    formatSeparator: ","
  },

  react: {
    wait: true
  }
});

export default i18n;