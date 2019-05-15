export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_SINGLE_CHECKLIST':
            return action.payload;
        default:
            return state;
    }
}