import { push } from 'react-router-redux';
import i18n from "../i18n";

export const GET_ALL_APARTMENTS = 'GET_ALL_APARTMENTS';
export const GET_ALL_AUTHENTICATIONS = 'GET_ALL_AUTHENTICATIONS';
export const GET_ALL_EMPLOYEES = 'GET_ALL_EMPLOYEES';
export const GET_AVAILABLE_EMPLOYEES = 'GET_AVAILABLE_EMPLOYEES';
export const GET_ALL_TRIPS = 'GET_ALL_TRIPS';
export const SIGN_UP_USER = 'SIGN_UP_USER';
export const GET_ALL_OFFICES = 'GET_ALL_OFFICES';
export const CREATE_TRIP = 'CREATE_TRIP';
export const GET_TRIP_BASIC = 'GET_TRIP_BASIC';


var BACKEND_BASE_URI;
if (process.env.NODE_ENV === 'production'){
     BACKEND_BASE_URI = process.env.REACT_APP_PROD_BASE_URI;
}
else{
    BACKEND_BASE_URI = process.env.REACT_APP_DEV_BASE_URI;
}
console.log(BACKEND_BASE_URI);

var headers = {
    "Content-Type": "application/json",
    "Accept": "application/json",
    "X-Requested-With": "XMLHttpRequest",
};

export const getAllApartments = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/apartments`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_APARTMENTS,
                    payload: data
                });
            }).catch((error) => console.warn(error));
    }).catch((error) => console.warn(error));
}

export const getAllOffices = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/offices`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_OFFICES,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getAllAuthentication = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/authentication`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_AUTHENTICATIONS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getAllEmployees = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/profiles`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_EMPLOYEES,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getAvailableEmployees = (departureDate, returnDate) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                console.warn(data);
                dispatch({
                    type: GET_AVAILABLE_EMPLOYEES,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getAllTrips = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/travels`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_TRIPS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getBasicTrip = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/tripinfo/` + tripId +'/basic', {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_TRIP_BASIC,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const signUpUser = (FirstName, LastName, Email, Password, Office) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/employees", {
        method: "POST",
        body: JSON.stringify({ FirstName, LastName, Email, Password, Office }),
        headers,
    }).then(response => {
        if (response.status === 200) {
            response.json().then(data => {
                dispatch({
                    type: SIGN_UP_USER,
                    payload: data
                }).then(() => {
                    dispatch(push('/'))
                });
            });
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}


export const createTrip = (UsersId, OfficeId, DepartureDate, ReturnDate) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/trip", {
        method: "POST",
        body: JSON.stringify({ UsersId, OfficeId, DepartureDate, ReturnDate }),
        headers,
    }).then(response => {
        if (response.status === 200) {
            response.json().then(data => {
                dispatch({
                    type: CREATE_TRIP,
                    payload: data
                }).then(() => {
                    dispatch(push('/'))
                });
            });
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}