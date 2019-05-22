import { push } from 'react-router-redux';
import i18n from "../i18n";

export const GET_ALL_APARTMENTS = 'GET_ALL_APARTMENTS';
export const GET_ALL_AUTHENTICATIONS = 'GET_ALL_AUTHENTICATIONS';
export const GET_ALL_EMPLOYEES = 'GET_ALL_EMPLOYEES';
export const GET_AVAILABLE_EMPLOYEES = 'GET_AVAILABLE_EMPLOYEES';
export const GET_ALL_TRIPS = 'GET_ALL_TRIPS';
export const GET_ALL_OFFICES = 'GET_ALL_OFFICES';
export const CREATE_TRIP = 'CREATE_TRIP';
export const GET_TRIP_BASIC = 'GET_TRIP_BASIC';
export const GET_CHECKLIST = "GET_CHECKLIST";
export const GET_SINGLE_CHECKLIST = "GET_SINGLE_CHECKLIST";
export const POST_SINGLE_FLIGHT_INFO = "POST_SINGLE_FLIGHT_INFO";
export const DELETE_SINGLE_FLIGHT_INFO = "DELETE_SINGLE_FLIGHT_INFO";
export const POST_SINGLE_CAR_INFO = "POST_SINGLE_CAR_INFO";
export const DELETE_SINGLE_CAR_INFO = "DELETE_SINGLE_CAR_INFO";
export const CLEAR_CHECKLIST = "CLEAR_CHECKLIST";
export const GET_PLANS = "GET_PLANS";
export const CLEAR_TRIPS = "CLEAR_TRIPS";
export const CLEAR_MERGEABLE_TRIPS = "CLEAR_MERGEABLE_TRIPS";
export const GET_MERGEABLE_TRIPS = "GET_MERGEABLE_TRIPS";

var BACKEND_BASE_URI;
if (process.env.NODE_ENV === 'production') {
    BACKEND_BASE_URI = process.env.REACT_APP_PROD_BASE_URI;
}
else {
    BACKEND_BASE_URI = process.env.REACT_APP_DEV_BASE_URI;
}

export const getAllApartments = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/apartments`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
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
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
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

export const getAllEmployees = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
        }
        else {
            response.json()
                .then(data => {
                    dispatch({
                        type: GET_ALL_EMPLOYEES,
                        payload: data
                    });
                })
        }
    }).catch((error) => console.warn(error));
}

export const getEmployees = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_AVAILABLE_EMPLOYEES,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getAllTrips = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/tripinfo/organized`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
        }
        else {
            response.json()
                .then(data => {
                    dispatch({
                        type: GET_ALL_TRIPS,
                        payload: data
                    });
                });
        }
    }).catch((error) => console.warn(error));
}

export const getMergeableTrips = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/mergeableTrips/?firstTripId=` + tripId, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
        }
        else {
            response.json()
                .then(data => {
                    dispatch({
                        type: GET_MERGEABLE_TRIPS,
                        payload: data
                    });
                });
        }
    }).catch((error) => console.warn(error));
}

export const mergeTrips = (FirstTripId, SecondTripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/mergeTrips` , {
        method: "Post",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        },
        body: JSON.stringify({
            FirstTripId,
            SecondTripId
        }),
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if(response.status === 200){
            clearTrips();
            dispatch(push('/trips'));
            alert(i18n.t("TripMerged") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const getBasicTrip = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/tripinfo/` + tripId + '/basic', {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
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

export const getPlans = (employeeIds) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees/plans`, {
        method: "POST",
        body: JSON.stringify(employeeIds),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_PLANS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getChecklist = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/checklist', {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_CHECKLIST,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getSingleChecklist = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/checklist', {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        response.json()
            .then(data => {
                dispatch({
                    type: GET_SINGLE_CHECKLIST,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const updateFlightInfo = (flightInfo, employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight', {
        method: "PUT",
        body: JSON.stringify({
            isRequired: flightInfo.isRequired,
            flightNumber: flightInfo.flightNumber,
            company: flightInfo.company,
            flightTime: flightInfo.flightTime,
            airportAddress: flightInfo.airportAddress
        }),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            alert(i18n.t("FlightInfoUpdate") + response.status);
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}


export const createFlightInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight', {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            alert(i18n.t("FlightInfoCreate") + response.status);
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}


export const deleteFlightInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight', {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            alert(i18n.t("FlightInfoDelete") + response.status);
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const updateCarInfo = (carInfo, employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car', {
        method: "PUT",
        body: JSON.stringify({
            isRequired: carInfo.isRequired,
            carNumber: carInfo.carNumber,
            carAddress: carInfo.carAddress,
            rentEndTime: carInfo.rentEndTime,
            rentStartTime: carInfo.rentStartTime
        }),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            alert(i18n.t("CarInfoUpdate") + response.status);
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}


export const createCarInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car', {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            alert(i18n.t("CarInfoCreate") + response.status);
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const deleteCarInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car', {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            alert(i18n.t("CarInfoDelete") + response.status);
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const signUpUser = (FirstName, LastName, Email, Password, Office) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/employees", {
        method: "POST",
        body: JSON.stringify({ FirstName, LastName, Email, Password, Office }),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 200) {
            response.json().then(data => {
                sessionStorage.setItem('token', data.jwtToken)
                dispatch(push('/'))
            });
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const login = (Email, Password) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/authentication", {
        method: "POST",
        body: JSON.stringify({ Email, Password }),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
        }
    }).then(response => {
        if (response.status === 200) {
            response.json().then(data => {
                sessionStorage.setItem('token', data.jwtToken);
                dispatch(push('/'))
            });
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const createTrip = (employeeIds, destinationOfficeId, departureDate, returnDate) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/trips", {
        method: "POST",
        body: JSON.stringify({ employeeIds, destinationOfficeId, departureDate, returnDate }),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
    }).then(response => {
        if (response.status === 401) {
            dispatch(push('/login'));
            sessionStorage.removeItem('token');
            return;
        }
        if (response.status === 200) {
            response.json().then(data => {
                dispatch({
                    type: CREATE_TRIP,
                    payload: data
                })
                dispatch(push('/trip/' + data.tripId))
            });
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const clearChecklist = () => (dispatch) => {
    dispatch({
        type: CLEAR_CHECKLIST
    });
}

export const clearTrips = () => (dispatch) => {
    dispatch({
        type: CLEAR_TRIPS
    });
}

export const clearMergableTrips = () => (dispatch) => {
    dispatch({
        type: CLEAR_MERGEABLE_TRIPS
    });
}

export const logoutUser = () => (dispatch) => {
    sessionStorage.removeItem('token');
    dispatch(push('/login'));
}