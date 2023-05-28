import { FC } from 'react'
import { ILesson } from '../../../models/ILesson'
import { Button, ListItem, Typography } from '@mui/material';
import { useQueryClient, useMutation } from 'react-query';
import { deleteLesson } from './../../../services/lessons';

interface LessonsItemProps {
  lesson: ILesson
}

export const LessonsItem: FC<LessonsItemProps> = ({ lesson }) => {
  const { id, name, courseId } = lesson

  const client = useQueryClient();
  
  const {mutate: deleteLessonById} = useMutation({
    mutationFn: deleteLesson,
    onSuccess: () => {
      client.invalidateQueries({
        queryKey: ['lessons', 'all', 'search']
      })
    }
  })

  const buttonClickHandler = async () => {
    if (id === undefined) return
    deleteLessonById(id)
  }

  return (
    <ListItem sx={{gap: "10px"}}>
      <Typography>id: {id}</Typography>
      <Typography>name: {name}</Typography>
      <Typography>courseId: {courseId}</Typography>
			<Button variant="outlined" onClick={buttonClickHandler}>Удалить</Button>
    </ListItem>
  )
}
