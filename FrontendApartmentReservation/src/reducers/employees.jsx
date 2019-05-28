export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_ALL_EMPLOYEES':
            return action.payload;
        case 'GET_AVAILABLE_EMPLOYEES':
            return action.payload;
        case 'GET_EMPLOYEE_BY_ID':
            return action.payload;
        case "CLEAR_EMPLOYEES":
            return [];
        default:
            return state;
    }
}