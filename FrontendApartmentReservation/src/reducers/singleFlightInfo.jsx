export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'POST_SINGLE_FLIGHT_INFO':
            return action.payload;
        case 'DELETE_SINGLE_FLIGHT_INFO':
            return action.payload;
        default:
            return state;
    }
}