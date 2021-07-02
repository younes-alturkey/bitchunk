import React, { useEffect } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import Paper from '@material-ui/core/Paper'
import Grid from '@material-ui/core/Grid'
import Typography from '@material-ui/core/Typography'
import Grow from '@material-ui/core/Grow'
import ThanosGif from '../assets/img/thanos.gif'

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  rootForm: {
    '& > *': {
      margin: theme.spacing(1),
      width: '75%',
    },
  },
  paper: {
    padding: theme.spacing(2),
    marginBottom: theme.spacing(1),
    marginTop: theme.spacing(6),
    textAlign: 'center',
    color: theme.palette.text.secondary,
    borderRadius: theme.spacing(2),
  },
  large: {
    width: theme.spacing(23),
    height: theme.spacing(8),
    padding: theme.spacing(4),
    margin: 'auto',
  },
  divider: {
    borderBottom: 'solid',
    borderWidth: '1px',
    color: '#ff5722',
    padding: '0.5rem',
    marginBottom: '0.5rem',
    margin: 'auto',
    width: '1.5rem',
  },
  gif: {
    width: 325,
    height: 325,
    textAlign: 'center',
  },
}))

export default function How() {
  document.title = 'How It Works'
  const classes = useStyles()

  useEffect(() => {
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])
  return (
    <Grow direction="up" in={true}>
      <Container className={classes.root}>
        <Grid justify="center" container spacing={2}>
          <Grid item xs={12} lg={10} md={8}>
            <Paper className={classes.paper} elevation={20}>
              <Typography color="secondary" variant="h5" nowrap="true">
                Our Service In a Nutshell
              </Typography>
              <div className={classes.divider} />
              <Grid container justify="center" spacing={2}>
                <Grid item align="center" xs={12}>
                  <img alt="404 Error" src={ThanosGif} />
                </Grid>
                <Grid item align="center" xs={8}>
                  <Typography color="primary" variant="h5" nowrap="true">
                    Your files...
                  </Typography>
                </Grid>
              </Grid>
            </Paper>
          </Grid>
        </Grid>
      </Container>
    </Grow>
  )
}
