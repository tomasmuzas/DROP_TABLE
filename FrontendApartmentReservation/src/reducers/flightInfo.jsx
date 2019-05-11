export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'CLEAR_FLIGHT_INFO': 
            return [];
        case 'GET_FLIGHT_INFO':
            return [
                ...state,
                action.payload
            ];
        default:
            return state;
    }
}