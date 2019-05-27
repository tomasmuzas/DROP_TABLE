export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_ALL_EMPLOYEES':
            return action.payload;
        case 'GET_AVAILABLE_EMPLOYEES':
            return action.payload;
        default:
            return state;
    }
}