export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_ALL_USERS':
            return action.payload;
        case 'SIGN_UP_USER':
            return [
                ...state,
                action.payload
            ];
        case 'GET_AVAILABLE_EMPLOYEES':
            return action.payload;
        default:
            return state;
    }
}