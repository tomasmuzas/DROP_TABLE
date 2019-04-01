import { push } from 'react-router-redux';

export const GET_ALL_APARTMENTS = 'GET_ALL_APARTMENTS';
export const GET_ALL_AUTHENTICATIONS = 'GET_ALL_AUTHENTICATIONS';
export const GET_ALL_USERS = 'GET_ALL_USERS';
export const GET_ALL_TRIPS = 'GET_ALL_TRIPS';
export const SIGN_UP_USER = 'GET_ALL_TRIPS';


var headers = {
    "Content-Type": "application/json",
    "Accept": "application/json",
    "X-Requested-With": "XMLHttpRequest",
};

export const getAllApartments = () => (dispatch) => {
    console.log("index")
    return fetch(`https://backend-apartments-kibanaprod.1d35.starter-us-east-1.openshiftapps.com/api/apartments`, {
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
    return fetch(`https://backend-apartments-kibanaprod.1d35.starter-us-east-1.openshiftapps.com/api/authentication`, {
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

export const getAllUsers = () => (dispatch) => {
    console.log("indexas");
    return fetch(`https://backend-apartments-kibanaprod.1d35.starter-us-east-1.openshiftapps.com/api/profiles`, {
        method: "GET",
        headers: headers
    }).then(response => {
        if (!response.ok) {
            throw new Error("Bad response");
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_USERS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getAllTrips = () => (dispatch) => {
    return fetch(`https://backend-apartments-kibanaprod.1d35.starter-us-east-1.openshiftapps.com/api/travels`, {
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
    fetch("https://backend-apartments-kibanaprod.1d35.starter-us-east-1.openshiftapps.com/api/users/profiles", {
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
            alert('Something is broken. Sorry ' + response.status);
        }
    }).catch((error) => console.warn(error));
}

