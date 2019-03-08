export default function reducer(state = null, action = {}) {
    switch (action.type) {
        case 'GET_ALL_APARTMENTS':
            console.log(action);
            return action.payload;
        default:
            return state;
    }
}