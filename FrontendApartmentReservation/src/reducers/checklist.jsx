export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'CLEAR_CHECKLIST': 
            return [];
        case 'GET_CHECKLIST':
            return [
                ...state,
                action.payload
            ];
        default:
            return state;
    }
}