export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_ALL_OFFICES':
            return action.payload;
        default:
            return state;
    }
}