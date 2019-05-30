import { push } from 'react-router-redux';
import i18n from "../i18n";
import { Router } from 'react-router';
import uuid from 'uuid/v1'

export const GET_ALL_APARTMENTS = 'GET_ALL_APARTMENTS';
export const GET_ALL_AUTHENTICATIONS = 'GET_ALL_AUTHENTICATIONS';
export const GET_ALL_EMPLOYEES = 'GET_ALL_EMPLOYEES';
export const GET_AVAILABLE_EMPLOYEES = 'GET_AVAILABLE_EMPLOYEES';
export const GET_ALL_TRIPS = 'GET_ALL_TRIPS';
export const GET_ALL_OFFICES = 'GET_ALL_OFFICES';
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
export const GET_MY_TRIPS = "GET_MY_TRIPS";
export const CLEAR_MY_TRIPS = "CLEAR_MY_TRIPS";
export const UPDATE_MY_TRIPS = "UPDATE_MY_TRIPS";
export const DELETE_TRIP = "DELETE_TRIP";
export const GET_PERSONAL_CHECKLIST = "GET_PERSONAL_CHECKLIST";
export const CLEAR_PERSONAL_CHECKLIST = "CLEAR_PERSONAL_CHECKLIST";
export const GET_EMPLOYEE_BY_ID = 'GET_EMPLOYEE_BY_ID';
export const CLEAR_EMPLOYEES = 'CLEAR_EMPLOYEES';

var BACKEND_BASE_URI;
if (process.env.NODE_ENV === 'production') {
    BACKEND_BASE_URI = process.env.REACT_APP_PROD_BASE_URI;
}
else {
    BACKEND_BASE_URI = process.env.REACT_APP_DEV_BASE_URI;
}

export const BACKEND_URL = BACKEND_BASE_URI

const getDefaultHeaders = () => {
    return {
        "Content-Type": "application/json",
        "Accept": "application/json",
        "Authorization": "Bearer " + sessionStorage.getItem('token'),
        "x-correlation-id": uuid()
    }
}

const verifyAuthorization = (response, dispatch) => new Promise((resolve, reject) =>{    
    if (response.status === 401) {
        dispatch(push('/login'));
        sessionStorage.removeItem('token');
        reject("Bad response");
        return;
    }
    if (response.status === 403) {
        dispatch(push('/error'));
        sessionStorage.removeItem('token');
        reject("No rights");
        return
    }
    resolve(response)
});

const assertSuccessStatusCode = (response, dispatch) => new Promise((resolve, reject) => {
    if(response.status !== 200){
        if(response.errorCode){
            alert(i18n.t(response.errorCode));
            reject(response.errorCode);
            return;
        }
        else{
            alert(i18n.t("SomethingWentWrong"));
            reject(response.status);
            return;
        }
    }

    resolve(response);
})

export const getAllApartments = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/apartments`, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_APARTMENTS,
                    payload: data
                });
            }).catch((error) => console.warn(error));
    }).catch((error) => console.warn(error));
}

export const reserveApartmentsForAll = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + `/apartment`, {
        method: "Post",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        clearTrips();
        dispatch(push('/trip/' + tripId));
    }).catch((error) => console.warn(error));
}

export const reserveApartmentsForOne = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + `/apartment`, {
        method: "Post",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        clearTrips();
        return response.status;
    }).catch((error) => console.warn(error));
}

export const deleteApartmentsReservationForOne = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + `/apartment`, {
        method: "Delete",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        clearTrips();
        return response.status;   
    }).catch((error) => console.warn(error));
}

export const deleteApartmentsReservation = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + `/apartment`, {
        method: "Delete",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        clearTrips();
        dispatch(push('/trip/' + tripId));
    }).catch((error) => console.warn(error));
}

export const getAllOffices = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/offices`, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
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
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_EMPLOYEES,
                    payload: data
                });
            })
    }).catch((error) => console.warn(error));
}

export const getEmployees = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees`, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_AVAILABLE_EMPLOYEES,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getEmployeesWithRoles = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees/full`, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_AVAILABLE_EMPLOYEES,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getEmployeeById = (employeeId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/employees/full/` + employeeId, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_EMPLOYEE_BY_ID,
                    payload: data
                });
            });
        return response.status;
    }).catch((error) => console.warn(error));
}

export const getAllTrips = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/tripinfo/organized`, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_ALL_TRIPS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getMyTrips = () => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/tripinfo/participating`, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_MY_TRIPS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getMergeableTrips = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/mergeableTrips/?firstTripId=` + tripId, {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_MERGEABLE_TRIPS,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const mergeTrips = (FirstTripId, SecondTripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/mergeTrips`, {
        method: "Post",
        headers: getDefaultHeaders(),
        body: JSON.stringify({
            FirstTripId,
            SecondTripId
        }),
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        clearTrips();
        dispatch(push('/trips'));
        alert(i18n.t("TripMerged") + response.status);
    }).catch((error) => console.warn(error));
}

export const getBasicTrip = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/tripinfo/` + tripId + '/basic', {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
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
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
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
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_CHECKLIST,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getPersonalChecklist = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/checklist/personal', {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        response.json()
            .then(data => {
                dispatch({
                    type: GET_PERSONAL_CHECKLIST,
                    payload: data
                });
            });
    }).catch((error) => console.warn(error));
}

export const getSingleChecklist = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/checklist', {
        method: "GET",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
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
    var returnFlight = '';
    if (!!flightInfo.flightDate && !!flightInfo.flightTime) {
        returnFlight = flightInfo.flightDate + ' ' + flightInfo.flightTime;
     }

    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight', {
        method: "PUT",
        body: JSON.stringify({
            isRequired: flightInfo.isRequired,
            flightNumber: flightInfo.flightNumber,
            company: flightInfo.company,
            flightTime: returnFlight,
            airportAddress: flightInfo.airportAddress
        }),
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("FlightInfoUpdate"));
    }).catch((error) => console.warn(error));
}

export const uploadFlightTicket = (flightTicketFile, employeeId, tripId) => (dispatch) => {
    const formData = new FormData();
    formData.append('file', flightTicketFile);
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight/ticket', {
        method: "PUT",
        body: formData,
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem('token'),
            "x-correlation-id": uuid()
        }
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("FlightInfoUpdate"));
    }).catch((error) => console.warn(error));
}

export const uploadHotelDocuments = (flightTicketFile, employeeId, tripId) => (dispatch) => {
    const formData = new FormData();
    formData.append('file', flightTicketFile);
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/hotel/documents', {
        method: "PUT",
        body: formData,
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem('token'),
            "x-correlation-id": uuid()
        }
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("FlightInfoUpdate"));
    }).catch((error) => console.warn(error));
}

export const updateCarDocuments = (flightTicketFile, employeeId, tripId) => (dispatch) => {
    const formData = new FormData();
    formData.append('file', flightTicketFile);
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car/documents', {
        method: "PUT",
        body: formData,
        headers: {
            "Authorization": "Bearer " + sessionStorage.getItem('token'),
            "x-correlation-id": uuid()
        }
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("FlightInfoUpdate"));
    }).catch((error) => console.warn(error));
}


export const createFlightInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight', {
        method: "POST",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("FlightInfoCreate"));
    }).catch((error) => console.warn(error));
}

export const createApartmentsInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/hotel', {
        method: "POST",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("ApartmentsInfoCreated"));
    }).catch((error) => console.warn(error));
}

export const deleteHotelReservation = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/hotel', {
        method: "DELETE",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("ApartmentsInfoDeleted"));
        return response.status;
    }).catch((error) => console.warn(error));
}
export const deleteApartmentsInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/livingplace', {
        method: "DELETE",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("ApartmentsInfoDeleted"));
    }).catch((error) => console.warn(error));
}

export const updateApartmentsInfo = (HotelName, DateFrom, DateTo, tripId, employeeId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/hotel', {
        method: "PUT",
        body: JSON.stringify({ HotelName, DateFrom, DateTo }),
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("ApartmentsInfoUpdated") + response.status);
    }).catch((error) => console.warn(error));
}

export const deleteFlightInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/flight', {
        method: "DELETE",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("FlightInfoDelete"));
    }).catch((error) => console.warn(error));
}

export const updateCarInfo = (carInfo, employeeId, tripId) => (dispatch) => {
    var returnStart = '';
    var returnEnd = '';
    if (!!carInfo.rentEndDate && !!carInfo.rentEndTime) {
       returnEnd = carInfo.rentEndDate + ' ' + carInfo.rentEndTime;
    }

    if (!!carInfo.rentStartDate && !!carInfo.rentStartTime) {
        returnStart = carInfo.rentStartDate + ' ' + carInfo.rentStartTime;
    }
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car', {
        method: "PUT",
        body: JSON.stringify({
            isRequired: carInfo.isRequired,
            carNumber: carInfo.carNumber,
            carAddress: carInfo.carAddress,
            rentEndTime: returnEnd,
            rentStartTime: returnStart
        }),
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("CarInfoUpdate"));
    }).catch((error) => console.warn(error));
}

export const acceptTrip = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/participation/accept', {
        method: "POST",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        clearTrips();
        dispatch({
            type: UPDATE_MY_TRIPS,
            payload: tripId
        })
        return response.status;
    }).catch((error) => console.warn(error));
}

export const createCarInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car', {
        method: "POST",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("CarInfoCreate"));
    }).catch((error) => console.warn(error));
}

export const deleteCarInfo = (employeeId, tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId + '/employees/' + employeeId + '/car', {
        method: "DELETE",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        alert(i18n.t("CarInfoDelete"));
    }).catch((error) => console.warn(error));
}

export const deleteTrip = (tripId) => (dispatch) => {
    return fetch(BACKEND_BASE_URI + `/api/trips/` + tripId, {
        method: "DELETE",
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        dispatch({
            type: 'DELETE_TRIP',
            payload: tripId
        });
        return response.status;
    }).catch((error) => console.warn(error));
}

export const signUpUser = (FirstName, LastName, Email, Password, Office) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/employees", {
        method: "POST",
        body: JSON.stringify({ FirstName, LastName, Email, Password, Office }),
        headers: getDefaultHeaders()
    }).then(response => {
        if (response.status === 200) {
            response.json().then(data => {
                dispatch(push('/'))
            });
        }
        if (response.status === 403) {
            dispatch(push('/error'));
            sessionStorage.removeItem('token');
            throw new Error("No rights");
        }
        else {
            alert(i18n.t("SignUpError") + response.status);
        }
    }).catch((error) => console.warn(error));
}

export const updateUser = (FirstName, LastName, Email, OfficeId, Role, EmployeeId, Version) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/employees/" + EmployeeId + "/info", {
        method: "PUT",
        body: JSON.stringify({ FirstName, LastName, Email, OfficeId, Role, Version }),
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => {
        if (response.status === 200) {
            dispatch({
                type: CLEAR_EMPLOYEES
            });
            dispatch(push('/users'))
        }
        else if (response.status === 403) {
            dispatch(push('/error'));
            sessionStorage.removeItem('token');
            throw new Error("No rights");
        }
        else{
            const force = window.confirm(i18n.t("EmployeeConcurrency"))
            if(force){
                alert("You have chosen to overwrite")
            }
            else{
                window.location.reload()
            }
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
    })
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
            response.json().then(data => {
                sessionStorage.setItem('token', data.jwtToken);
                var usefulInfo = data.jwtToken.split('.');
                var decodedString = Buffer.from(usefulInfo[1], 'base64').toString();
                var decodedJson = JSON.parse(decodedString);
                var userRole = decodedJson["Role"];
                sessionStorage.setItem('role', userRole);
                dispatch(push('/myInfo/myTrips'))
            });
    }).catch((error) => console.warn(error));
}

export const createTrip = (employeeIds, destinationOfficeId, departureDate, returnDate) => (dispatch) => {
    fetch(BACKEND_BASE_URI + "/api/trips", {
        method: "POST",
        body: JSON.stringify({ employeeIds, destinationOfficeId, departureDate, returnDate }),
        headers: getDefaultHeaders()
    })
    .then(response => verifyAuthorization(response, dispatch))
    .then(response => assertSuccessStatusCode(response, dispatch))
    .then(response => {
        dispatch({
            type: CLEAR_PERSONAL_CHECKLIST
        });

        dispatch({
            type: CLEAR_MY_TRIPS
        });

        response.json().then(data => {
            dispatch(push('/trip/' + data.tripId))
        });
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

export const clearMyTrips = () => (dispatch) => {
    dispatch({
        type: CLEAR_MY_TRIPS
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

export const clearPersonalChecklist = () => (dispatch) => {
    dispatch({
        type: CLEAR_PERSONAL_CHECKLIST
    });
}