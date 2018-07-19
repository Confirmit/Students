const sortCandidates = (candidates, sortingField, sortingDirection) => {
  const sortResult = sortingDirection === 'desc' ? 1 : -1
  return candidates.sort((a, b) => {
    if (!a[sortingField] || a[sortingField] === null) {
      return sortResult
    }
    if (!b[sortingField] || b[sortingField] === null) {
      return -sortResult
    }
    switch (sortingField) {
      case 'learningStart':
        return sortResult * sortByDate(a[sortingField], b[sortingField])
      case 'learningEnd':
        return sortResult * sortByDate(a[sortingField], b[sortingField])
      case 'interviewDate':
        return sortResult * sortByDateTime(a[sortingField], b[sortingField])
      default:
        return sortResult * sortByAlphabet(a[sortingField], b[sortingField])
    }
  })
}

const sortByAlphabet = (a, b) => {
  if (a.toLowerCase() > b.toLowerCase()) {
    return 1
  } else {
    if (a.toLowerCase() < b.toLowerCase()) {
      return -1
    } else {
      return 0
    }
  }
}

const sortByDate = (a, b) => {
  const aDate = a.split('.')
  const aResult = Number(aDate[2])*10000 + Number(aDate[1])*100 + Number(aDate[0])

  const bDate = b.split('.')
  const bResult = Number(bDate[2])*10000 + Number(bDate[1])*100 + Number(bDate[0])

  if (aResult > bResult) {
    return 1
  } else {
    if (aResult < bResult) {
      return -1
    } else {
      return 0
    }
  }
}

const sortByDateTime = (a, b) => {
  const aSplited = a.split(' ')
  const aTime = aSplited[0].split(':')
  if (!aTime[2])
    aTime[2] = '00'
  const aDate = aSplited[1].split('.')
  const aResult = Number(aDate[2])*1000000000000 + Number(aDate[1])*1000000000 + Number(aDate[0])*1000000 + Number(aTime[0])*10000 + Number(aTime[1])*100 + Number(aTime[2])

  const bSplited = b.split(' ')
  const bTime = bSplited[0].split(':')
  if (!bTime[2])
    bTime[2] = '00'
  const bDate = bSplited[1].split('.')
  const bResult = Number(bDate[2])*1000000000000 + Number(bDate[1])*1000000000 + Number(bDate[0])*1000000 + Number(bTime[0])*10000 + Number(bTime[1])*100 + Number(bTime[2])

  if (aResult > bResult) {
    return 1
  } else {
    if (aResult < bResult) {
      return -1
    } else {
      return 0
    }
  }
}

export default sortCandidates