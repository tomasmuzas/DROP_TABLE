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
        "Trips" : "Trips",
        "Users" : "Users",
        "SignUp": "Sign up!",
        "Name" : "Name",
        "Surname" : "Surname",
        "Email": "Email",
        "Password": "Password",
        "Office" : "Office",
        "SignUpError": "Error:",
        "PasswordError": "Password must contain one uppercase one lowercase, special symbol and be 5-15 chars length",
        "CreateTrip": "Create a trip",
        "SelectEmployees": "Select employees",
        "SelectOffice" : "Select DB office",
        "Cost" : "Cost",
        "DepartureDate" : "Departure time",
        "ReturnDate": "Return date"
      }
    },
    lt: {
      translations: {
        Text: "translation", 
        "Apartments": "Apartamentai",
        "Authentication": "Autentikacija",
        "Trips" : "Keliones",
        "Users" : "Naudotojai",
        "SignUp": "Užsiregistruoti",
        "Name" : "Vardas",
        "Surname" : "Pavardė",
        "Email": "Elektroninis paštas",
        "Password": "Slaptažodis",
        "Office" : "Ofisas",
        "SignUpError": "Klaida registruojantis:",
        "PasswordError": "Slaptažodis turi turėti vieną didžiąją vieną ir mažąją raides, specialų simbolį ir būti 5-15 simbolių ilgio",
        "CreateTrip": "Sukurti kelionę",
        "SelectEmployees": "Pasirinkite darbuotojus",
        "SelectOffice" : "Pasirinkite DB offisą",
        "Cost" : "Kaina",
        "DepartureDate" : "Išvykimo data",
        "ReturnDate": "Grįžimo data"
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