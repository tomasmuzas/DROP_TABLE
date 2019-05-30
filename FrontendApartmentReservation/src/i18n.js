import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";

i18n.use(LanguageDetector).init({
  // we init with resources
  resources: {
    en: {
      translations: {
        Text: "translation",
        "Administration": "Administration",
        "TripAdministration": "Trip Management",
        "Apartments": "Apartments",
        "Authentication": "Authentication",
        "Trips": "Organized Trips",
        "Users": "Users",
        "SignUp": "Sign up employee", 
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
        "ReserveApartmentForAll": "Reserve for all",
        "TripEnoughApartments": "There are enough available rooms in the apartment to accomodate all travelers.",
        "MyOrganizedTrips": "My organized trips",        
        "TripStart": "From",
        "TripEnd": "to",
        "TripDestination": "Trip to",
        "TripAccepted": "Accepted",
        "TripNotYetAccepted": "Not yet accepted",
        "FlightRequired": "Is flight required?",
        "FinishCreation": "Finish",
        "FlightNumber": "Flight Number",
        "FlightTime": "Flight Time",
        "FlightTicket": "Flight Ticket",
        "HotelDocuments": "Hotel documents",
        "CurrentFlightTicket": "Current flight ticket",
        "AirportAddress": "Airport Address",
        "FlightCompany": "Flight Company",
        "SaveFlightInfo": "Save flight info",
        "SaveTicket": "Save flight ticket",
        "SaveCarInfo": "Save car info",
        "SaveApartmentsInfo": "Save apartments info",
        "ApartmentsAddres": "Apartments Address",
        "FlightInfoUpdate": "Flight info updated",
        "FlightInfoCreate": "Flight info created",
        "FlightInfoDelete": "Flight info deleted",
        "CarInfoUpdate": "Car info updated",
        "CarInfoCreate": "Car info created",
        "CarInfoDelete": "Car info deleted",
        "CarInformation": "Car rent information",
        "HotelInformation": "Hotel information",
        "ApartmentInformation": "Apartment information",
        "FlightInformation": "Flight information",
        "CarDocuments": "Car documents",
        "SaveDocuments": "Save car documents",        
        "CurrentCarDocuments": "Current car documents",
        "CurrentHotelDocuments": "Current hotel documents",
        "SaveHotelDocuments": "Save hotel documents",
        "ApartmentsInfoUpdated":"Apartments info updated",
        "DestinationOffice": "Destination Office",
        "CarNumber": "Car Number",
        "CarAddress": "Car address",
        "RentEndTime": "End Time of Rent",
        "RentStartTime": "Start Time of Rent",
        "GoBack": "Go back",
        "ErrorDateMessage": "Selected range must not overlap with disabled dates.",
        "Login": "Login",
        "Logout": "Logout",
        "Merge": "Merge",
        "TripInformation": "Trip information",
        "MyTripToMerge" : "My trip to merge",
        "TripsPossibleToMerge" : "Trips possible to merge",
        "TripMerged": "Trips merged",
        "MyTrips": "My trips",
        "NoTripsToMerge": "There are no trips possible to merge",
        "Accept": "Accept",
        "DeleteTrip" : "Delete Trip",
        "ApartmentAddress" : "Apartments Address",
        "RoomNumber" : "Room Number",
        "DateFrom": "Date From",
        "DateTo" :"Date To",
        "Hotel" : "Hotel",
        "SaveHotelInfo": "Save hotel information",
        "AvailableSpaceInApartments" : "Available space in apartments: ",
        "ReserveApartmentForOne" : "Reserve apartment",
        "DeleteReservation" : "Delete reservation",
        "ReservationAlreadyMade" : "Reservation is already made",
        "ApartmentsInfoUpdated":"Apartments information updated",
        "ApartmentsInfoCreated":"Apartments information created",
        "ApartmentsInfoDeleted":"Apartments information deleted",
        "BookHotel" : "Book hotel",
        "DeleteHotelInfo": "Delete hotel information",
        "Employees" : "Employees",
        "Employee" : "Employee",
        "EmptyEmployeesList": "Employees list is empty",
        "EmptyTripsList": "Trips list is empty",
        "Admin": "Admin",
        "Organizer":"Organizer",
        "Regular": "Regular",
        "SelectEmployeeRole":"Select employee role",
        "EditEmployeeInfo": "Edit employee info",
        "EmployeeOverwriteWarning": "While you were editing employee information, it has changed. If you want to overwrite, press \"OK\", else - \"Cancel\". Your changes will be lost."
        "Price": "Price",
        "TotalPriceCost": "Total price of this trip: "
      }
    },
    lt: {
      translations: {
        Text: "translation",
        "Administration": "Administravimas",
        "TripAdministration": "Kelionių organizavimas",
        "Apartments": "Apartamentai",
        "Authentication": "Autentikacija",
        "Trips": "Organizuojamos Kelionės",
        "Users": "Naudotojai",
        "SignUp": "Užregistruoti darbuotoją",
        "Name": "Vardas",
        "Surname": "Pavardė",
        "Email": "Elektroninis paštas",
        "Password": "Slaptažodis",
        "Office": "Ofisas",
        "SignUpError": "Klaida registruojantis:",
        "PasswordError": "Slaptažodis privalo turėti bent po vieną didžiąją ir mažąją raides, specialų simbolį ir būti 5-15 simbolių ilgio",
        "CreateTrip": "Sukurti kelionę",
        "SelectEmployees": "Pasirinkite darbuotojus",
        "SelectOffice": "Pasirinkite DB offisą",
        "Cost": "Kaina",
        "DepartureDate": "Išvykimo data",
        "ReturnDate": "Grįžimo data",
        "CarRequired": "Ar mašina reikalinga?",
        "ApartmentRequired": "Ar apartamentai reikalingi?",
        "MyOrganizedTrips": "Mano organizuojamos kelionės",
        "TripStart": "Nuo",
        "TripEnd": "iki",
        "TripEnoughApartments": "Apartamentuose užtenka vietos apgyvendinti visus keliautojus.",
        "ReserveApartmentForAll": "Užsakyti visiems iš karto",
        "TripDestination": "Kelionė į",
        "TripAccepted": "Patvirtinta",
        "TripNotYetAccepted": "Dar nepatvirtinta",
        "FlightRequired": "Ar skrydžiai reikalingi?",
        "FinishCreation": "Užbaigti",
        "FlightNumber": "Skrydžio numeris",
        "FlightTime": "Skrydžio laikas",        
        "FlightTicket": "Skrydžio bilietas",
        "HotelDocuments": "Viešbučio dokumentai",
        "CurrentHotelDocuments": "Dabartiniai viešbučio dokumentai",
        "CurrentFlightTicket": "Dabartinis lėktuvo bilietas",
        "SaveHotelDocuments": "Išsaugoti viešbučio dokumentus",
        "AirportAddress": "Oro uosto adresas",
        "FlightCompany": "Skrydžių kompanija",
        "SaveFlightInfo": "Saugoti skrydžio informaciją",
        "SaveTicket": "Saugoti bilietą",
        "SaveCarInfo": "Saugoti mašinos informaciją",
        "SaveApartmentsInfo": "Saugoti apartamentų informaciją",
        "ApartmentsAddres": "Apartamentų adresas",
        "FlightInfoUpdate": "Skrydžio informacija išsaugota",
        "FlightInfoCreate": "Skrydžio informacija sukurta",
        "FlightInfoDelete": "Skrydžio informacija ištrinta",
        "CarInfoUpdate": "Mašinos informacija išsaugota",
        "CarInfoCreate": "Mašinos informacija sukurta",
        "CarInfoDelete": "Mašinos informacija ištrinta",
        "CarInformation": "Mašinos nuomos informacija",
        "HotelInformation": "Viešbučio informacija",
        "ApartmentInformation": "Apartamentų informacija",
        "FlightInformation": "Skrydžio informacija",        
        "CarDocuments": "Mašinos dokumentai",
        "SaveDocuments": "Išsaugoti mašinos dokumentus",        
        "CurrentCarDocuments": "Dabartiniai mašinos dokumentai",
        "DestinationOffice": "Išvykimo ofisas",
        "CarNumber": "Mašinos numeris",
        "CarAddress": "Mašinos adresas",
        "RentEndTime": "Nuomos pabaiga",
        "RentStartTime": "Nuomos pradžia",
        "GoBack": "Grįžti atgal",
        "Monday": "P",
        "Tuesday": "A",
        "Wednesday": "T",
        "Thursday": "K",
        "Friday": "Pn",
        "Saturday": "Š",
        "Sunday": "S",
        "ErrorDateMessage": "Pasirinktomis dienomis darbuotojai(as) yra užtimti",
        "Login": "Prisijungti",
        "Logout": "Atsijungti",
        "Merge": "Sujungti",
        "TripInformation": "Kelionės informacija",
        "MyTripToMerge" : "Mano kelionė kurią noriu sujungti",
        "TripsPossibleToMerge" : "Kelionės su kuriomis galima sujungti",
        "TripMerged": "Kelionės sujungtos",
        "MyTrips": "Mano kelionės",
        "NoTripsToMerge" : "Nėra kelionių kurias galite sujungti",
        "Accept" :"Patvirtinti",
        "DeleteTrip" : "Ištrinti kelionę",
        "ApartmentAddress" : "Apartamentų adresas",
        "RoomNumber" : "Kambario numeris",
        "DateFrom": "Pradžios data",
        "DateTo" :"Pabaigos data",
        "Hotel" : "Viešbutis",
        "SaveHotelInfo": "Išsaugoti viešbučio informaciją",
        "AvailableSpaceInApartments" : "Laisva vieta apartamentuose: ",
        "ReserveApartmentForOne" : "Rezervuoti apartamentus",
        "DeleteReservation" : "Ištrinti rezervaciją",
        "ReservationAlreadyMade" : "Apartamentai jau rezervuoti",
        "ApartmentsInfoUpdated":"Apartamentų informacija išsaugota",
        "ApartmentsInfoCreated":"Apartamentų informacija sukurta",
        "ApartmentsInfoDeleted":"Apartamentų informacija ištrinta",
        "BookHotel" : "Užsakyti viešbutį",
        "DeleteHotelInfo": "Ištrinti viešbučio informaciją",
        "Employees" : "Darbuotojai",
        "Employee" : "Darbuotojas",
        "EmptyEmployeesList": "Darbuotojų sąrašas tuščias",
        "EmptyTripsList": "Kelionių sąrašas tuščias",
        "Admin": "Administratorius",
        "Organizer":"Organizatorius",
        "Regular": "Regularus darbuotojas",
        "SelectEmployeeRole":"Pasirinkite darbuotojo rolę",
        "EditEmployeeInfo": "Redaguoti darbuotojo informaciją",
        "EmployeeOverwriteWarning": "Kol jūs redagavote vartotojo informaciją, ji buvo pakeista. Jeigu norite perrašyti esamus duomenis, spaskite \"OK\", jei ne - \"Atšaukti\". Jūsų pakeitimai bus prarasti."
        "Price": "Kaina",
        "TotalPriceCost": "Visos kelionės kaina: "
      }
    }
  },
  fallbackLng: "en",
  debug: false,

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