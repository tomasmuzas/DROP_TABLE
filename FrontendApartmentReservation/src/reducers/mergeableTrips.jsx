export default function reducer(state = [], action = {}) {
    switch (action.type) {
        case 'GET_MERGEABLE_TRIPS':
            return action.payload;
        case 'CLEAR_MERGEABLE_TRIPS':
            return []
        default:
            return state;
    }
}