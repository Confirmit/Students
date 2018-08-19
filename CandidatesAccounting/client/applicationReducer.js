import { handleActions } from 'redux-actions'
import * as application from './applicationActions'
import * as candidates from './candidates/actions'
import * as comments from './comments/actions'
import * as authorization from './authorization/actions'

const initialState = {
  initializing: true,
  fetching: false,
  pageTitle: 'Candidate Accounting',
  errorMessage: '',
  searchRequest: '',
}

const reducer = handleActions(
  {
    [application.initSuccess]: (state, {payload}) => ({
      ...state,
      ...initialState,
      searchRequest: payload.initialState.searchRequest ? payload.initialState.searchRequest : initialState.searchRequest
    }),

    [application.enableInitializing]: state => ({
      ...state,
      initializing: true
    }),

    [application.enableFetching]: state => ({
      ...state,
      fetching: true
    }),

    [application.disableFetching]: state => ({
      ...state,
      fetching: false
    }),

    [application.setSearchRequest]: (state, {payload}) => ({
      ...state,
      searchRequest: payload.searchRequest
    }),

    [application.resetSearchRequest]: state => ({
      ...state,
      searchRequest: ''
    }),

    [application.setErrorMessage]: (state, {payload}) => ({
      ...state,
      errorMessage: payload.message
    }),

    [candidates.setCandidateStatusSuccess]: (state, {payload}) => ({
      ...state,
      searchRequest: payload.status === state.candidateStatus ? '' : state.searchRequest
    }),

    [candidates.getCandidatesSuccess]: (state) => ({
      ...state,
      pageTitle: 'Candidate Accounting',
      initializing: false,
      fetching: false
    }),

    [comments.openCommentPageSuccess]: (state, {payload}) => ({
      ...state,
      pageTitle: payload.candidate.name,
      initializing: false,
      fetching: false
    }),

    [authorization.loginFailure]: (state, {payload}) => ({
      ...state,
      errorMessage: payload.error + '. Login failure.'
    }),

    [authorization.logoutFailure]: (state, {payload}) => ({
      ...state,
      errorMessage: payload.error + '. Logout failure.'
    })
  },
  initialState
)

export const SELECTORS = {
  INITIALIZING: state => state.initializing,
  FETCHING: state => state.fetching,
  PAGETITLE: state => state.pageTitle,
  ERRORMESSAGE: state => state.errorMessage,
  SEARCHREQUEST: state => state.searchRequest,
}

export default reducer