export default (state = 0, action) => {
    switch (action.type) {
        case 'GET_ALL_TRIPS':
            return action.payload;
        case 'CREATE_TRIP':
            return action.payload;
        case 'CLEAR_TRIPS':
            return [];
        default:
            return state;
    }
};

