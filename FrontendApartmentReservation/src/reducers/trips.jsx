export default (state = 0, action) => {
    switch (action.type) {
        case 'GET_ALL_TRIPS':
            return action.payload;
        case 'CREATE_TRIP':
            return [
                ...state,
                action.payload
            ];
        default:
            return state;
    }
};

