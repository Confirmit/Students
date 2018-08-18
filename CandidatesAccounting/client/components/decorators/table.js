import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import Paper from '@material-ui/core/Paper'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import TableCell from '@material-ui/core/TableCell'
import TableFooter from '@material-ui/core/TableFooter'

const CustomTable = (props) => {
  const headers = props.headers.map((header, index) =>
    <TableCell key={'th' + index}>{ header }</TableCell>
  )

  let rows = props.rows.map((row, index) =>
    <TableRow key={'tr' + index}>
      {row.cells.map((cell, cellIndex) =>
        <TableCell key={'td' + cellIndex} classes={{root: row.isDisabled ? 'disabled-cell' : ''}}>{cell}</TableCell>)}
    </TableRow>
  )
  if (rows.size === 0 || rows.length === 0) {
    rows =
      <TableRow>
        <TableCell>
          <EmptyTableMessageWrapper>
            The table is empty
          </EmptyTableMessageWrapper>
        </TableCell>
      </TableRow>
  }

  return (
    <Paper style={{ width: '100%', overflowX: 'auto' }}>
      <Table >
        <TableHead>
          <TableRow>
            {headers}
          </TableRow>
        </TableHead>
        <TableBody>
          {rows}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TableCell colSpan={props.headers.length}>
              {props.footer}
            </TableCell>
          </TableRow>
        </TableFooter>
      </Table>
    </Paper>
  )

}

CustomTable.propTypes = {
  headers: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  rows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  footer: PropTypes.object.isRequired
}

export default CustomTable

const EmptyTableMessageWrapper = styled.div`
  color: #aaa;
  text-align: center;
  position: absolute;
  width: 100%;
  margin-top: -12px;
`