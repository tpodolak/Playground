import * as types from './actionTypes';
import authorAPi from '../api/mockAuthorApi';
import {beginAjaxCall, ajaxCallError} from './ajaxStatusActions';

function loadAuthorsSuccess(authors){
    return {
        type: types.LOAD_AUTHORS_SUCCESS,
        authors
    };
}

export function loadAuthors(){
    return function (dispatch) {
        dispatch(beginAjaxCall());
        return authorAPi.getAllAuthors().then(authors => {
            dispatch(loadAuthorsSuccess(authors));
        }).catch(error => {
            dispatch(ajaxCallError(error));
            throw (error);
        });
    };
}