export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'CLEAR_PERSONAL_CHECKLIST':
            return [];
        case 'GET_PERSONAL_CHECKLIST':
            return [
                ...state,
                action.payload
            ];
        default:
            return state;
    }
}