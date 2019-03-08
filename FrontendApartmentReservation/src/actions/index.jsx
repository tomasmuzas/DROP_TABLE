
export const GET_ALL_APARTMENTS = 'GET_ALL_APARTMENTS';
export const GET_ALL_AUTHENTICATIONS = 'GET_ALL_AUTHENTICATIONS';
export const GET_ALL_USERS = 'GET_ALL_USERS';
export const GET_ALL_TRIPS = 'GET_ALL_TRIPS';


var headers = {
    "Content-Type": "application/json",
    "Accept": "application/json",
    "X-Requested-With": "XMLHttpRequest",
};

export const getAllApartments = () => (dispatch) => {
    console.log("index")
    return fetch(`https://localhost:44334/api/apartments`, {
        method: "GET",
        headers: headers,
        credentials: 'include'
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
    return fetch(`/api/authentications/`, {
        method: "GET",
        headers: headers,
        credentials: 'include'
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
    return fetch(`/api/users/`, {
        method: "GET",
        headers: headers,
        credentials: 'include'
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
    return fetch(`/api/authentications/`, {
        method: "GET",
        headers: headers,
        credentials: 'include'
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
