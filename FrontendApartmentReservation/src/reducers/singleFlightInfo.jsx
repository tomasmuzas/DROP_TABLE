export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_SINGLE_FLIGHT_INFO':
            return action.payload;
        case 'POST_SINGLE_FLIGHT_INFO':
            return action.payload;
        case 'DELETE_SINGLE_FLIGHT_INFO':
            return action.payload;
        case 'PUT_SINGLE_FLIGHT_INFO':
            return action.payload;
        default:
            return state;
    }
}