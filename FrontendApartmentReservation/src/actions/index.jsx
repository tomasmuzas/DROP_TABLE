import { push } from 'react-router-redux';
import i18n from "../i18n";

export const GET_ALL_APARTMENTS = 'GET_ALL_APARTMENTS';
export const GET_ALL_AUTHENTICATIONS = 'GET_ALL_AUTHENTICATIONS';
export const GET_ALL_EMPLOYEES = 'GET_ALL_EMPLOYEES';
export const GET_ALL_TRIPS = 'GET_ALL_TRIPS';
export const SIGN_UP_USER = 'GET_ALL_TRIPS';

var BACKEND_BASE_URI;
if (process.env.NODE_ENV !== 'production'){
     BACKEND_BASE_URI = process.env.REACT_APP_PROD_BASE_URI;
}
else{
    BACKEND_BASE_URI = process.env.REACT_APP_DEV_BASE_URI;
}

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

export const signUpUser = (FirstName, LastName, Email, Password, Office) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/users/profiles", {
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