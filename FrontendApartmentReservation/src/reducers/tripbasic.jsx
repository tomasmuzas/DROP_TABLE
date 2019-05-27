export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_TRIP_BASIC':
            return action.payload;
        default:
            return state;
    }
}