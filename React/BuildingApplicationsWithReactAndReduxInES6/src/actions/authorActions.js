import * as types from './actionTypes';
import authorAPi from '../api/mockAuthorApi';

function loadAuthorsSuccess(authors){
    return {
        type: types.LOAD_AUTHORS_SUCCESS,
        authors
    };
}

export function loadAuthors(){
    return function (dispatch) {
        return authorAPi.getAllAuthors().then(authors => {
            dispatch(loadAuthorsSuccess(authors));
        }).catch(error => {
            throw (error);
        });
    };
}