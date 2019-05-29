export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_ALL_TRIPS':
            return action.payload;
        case 'CLEAR_TRIPS':
            return [];
        case 'DELETE_TRIP':
            var deletedTripIndex = state.findIndex(trip => trip.tripId === action.payload);
            var stateCopy = state;
            stateCopy.splice(deletedTripIndex, 1);
            return stateCopy;
        default:
            return state;
    }
};

