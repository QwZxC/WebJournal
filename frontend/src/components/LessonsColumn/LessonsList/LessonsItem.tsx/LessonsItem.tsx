import { FC, useState } from 'react'
import { ILesson } from '../../../../models/ILesson'
import { IconButton, ListItem, SxProps, Theme, Typography } from '@mui/material'
import { useQueryClient, useMutation } from 'react-query'
import { deleteLesson, updateLesson } from '../../../../services/lessons'
import CreateIcon from '@mui/icons-material/Create'
import DeleteIcon from '@mui/icons-material/Delete'
import { useNavigate } from 'react-router-dom'
import { lessonUrl } from '../../../../constants/routes'
import { LessonUpdateForm } from './LessonUpdateForm/LessonUpdateForm'

interface LessonsItemProps {
  lesson: ILesson
  currentLessonId: number | undefined
}

export const LessonsItem: FC<LessonsItemProps> = ({
  lesson,
  currentLessonId,
}) => {
  const { id, name } = lesson

  const [isEditing, setIsEditing] = useState(false)

  const client = useQueryClient()
  const navigate = useNavigate()

  const { mutate: lessonUpdate } = useMutation({
    mutationFn: updateLesson,
    onSuccess: () => {
      client.invalidateQueries({
        queryKey: ['lessons', 'all', 'search'],
      })
    },
  })

  const { mutate: deleteLessonById } = useMutation({
    mutationFn: deleteLesson,
    onSuccess: () => {
      client.invalidateQueries({
        queryKey: ['lessons', 'all', 'search'],
      })
    },
  })

  const handleEditClick = () => {
    setIsEditing(true)
  }

  const handleDeleteClick = () => {
    if (id === undefined) return
    deleteLessonById(id)
  }

  const handleFormSubmit = (value: string) => {
    const newLesson = lesson
    newLesson.name = value
    lessonUpdate(newLesson)
    setIsEditing(false)
  }

  const handleFormCancel = () => {
    setIsEditing(false)
  }

  const handleClick = () => {
    navigate(lessonUrl + lesson?.id, { replace: true })
  }

  const commonStyles: SxProps<Theme> = {
    cursor: 'pointer',
    "&:hover": {
      backgroundColor: 'rgba(0, 0, 0, 0.1)'
    }
  }

  const lessonItemStyles: SxProps<Theme> =
    currentLessonId === lesson.id
      ? { backgroundColor: 'rgba(0, 0, 0, 0.1)', ...commonStyles }
      : commonStyles

  return (
    <ListItem onClick={handleClick} sx={lessonItemStyles}>
      {isEditing && (
        <LessonUpdateForm
          initialValue={name}
          onSubmit={handleFormSubmit}
          onCancel={handleFormCancel}
        />
      )}

      {!isEditing && (
        <>
          <Typography sx={{ flexGrow: 1 }}>{name}</Typography>

          <IconButton onClick={handleDeleteClick}>
            <DeleteIcon />
          </IconButton>

          <IconButton onClick={handleEditClick}>
            <CreateIcon />
          </IconButton>
        </>
      )}
    </ListItem>
  )
}
