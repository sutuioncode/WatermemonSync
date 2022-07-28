// @flow

import React from 'react'
import { render } from 'react-dom'

import DatabaseProvider from '@nozbe/watermelondb/DatabaseProvider'


import Root from 'components/Root'

import {database} from 'models/database'

render(
  <DatabaseProvider database={database}>
    <Root />
  </DatabaseProvider>, document.getElementById('application')
)
