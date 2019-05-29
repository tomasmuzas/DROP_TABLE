export default function reducer(state = [], action = {}) {
        switch (action.type) {
        case 'GET_MY_TRIPS':
            return action.payload;
        case 'CLEAR_MY_TRIPS':
            return [];
        case 'UPDATE_MY_TRIPS':
            var updatedTripIndex = state.findIndex(trip => trip.tripId === action.payload);
            var stateCopy = state;
            stateCopy[updatedTripIndex].checklistInfos[0].hasAcceptedTripConfirmation = true;
            return stateCopy;
        default:
            return state;
    }
};