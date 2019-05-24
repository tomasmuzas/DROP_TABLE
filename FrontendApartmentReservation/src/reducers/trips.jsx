export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_ALL_TRIPS':
            return action.payload;
        case 'CREATE_TRIP':
            return action.payload;
        case 'CLEAR_TRIPS':
            return null;
        default:
            return state;
    }
};

