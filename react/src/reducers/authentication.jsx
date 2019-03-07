export default function reducer(state = [], action = {}) {
  switch (action.type) {
      case 'GET_ALL_AUTHENTICATIONS':
          return action.payload;
      default:
          return state;
  }
}