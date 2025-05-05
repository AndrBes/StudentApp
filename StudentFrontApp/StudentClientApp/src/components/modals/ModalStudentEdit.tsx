import { request, useModel } from '@umijs/max';
import { Button, Form, Input, Modal, Select } from 'antd';
import { useState } from 'react';
import type { DraggableData, DraggableEvent } from 'react-draggable';
import Draggable from 'react-draggable';


export default (props: any) => {
    const { dataSource, bounds, draggleRef, groupOptions, formEdit, disabledEdit,openEdit, setBounds, setModalEditOpenState, setEditDisabledState, setDataSourceState  } = useModel('useStudentModel')

    const onStudentEdit = (data: any) => {
        console.log(data)
        request('/api/Student/Post', { data, method: 'POST'}).then((result: any) => {
        setModalEditOpenState(false)
        //message.success("Данные студента обновлены")
    
        const newStudents = dataSource.map((item: any) => {
          if(result.studentId == item.studentId) return result;
          return item
        })

        setDataSourceState(newStudents)
        })};

    const onStart = (_event: DraggableEvent, uiData: DraggableData) => {
        const { clientWidth, clientHeight } = window.document.documentElement;
        const targetRect = draggleRef.current?.getBoundingClientRect();
        if (!targetRect) {
          return;
        }
        setBounds({
          left: -targetRect.left + uiData.x,
          right: clientWidth - (targetRect.right - uiData.x),
          top: -targetRect.top + uiData.y,
          bottom: clientHeight - (targetRect.bottom - uiData.y),
        });
      };

    return (
        <>
        <Modal
        title={
          <div
            style={{ width: '100%', cursor: 'move' }}
            onMouseOver={() => {
              if (disabledEdit) {
                setEditDisabledState(false);
              }
            }}
            onMouseOut={() => {
              setEditDisabledState(true);
            }}
            // fix eslintjsx-a11y/mouse-events-have-key-events
            // https://github.com/jsx-eslint/eslint-plugin-jsx-a11y/blob/master/docs/rules/mouse-events-have-key-events.md
            onFocus={() => {}}
            onBlur={() => {}}
            // end
          >
            Редактирование студента
          </div>
        }
        open={openEdit}
        onOk={(e: React.MouseEvent<HTMLElement>) => {setModalEditOpenState(false)}}
        onCancel={(e: React.MouseEvent<HTMLElement>) => {setModalEditOpenState(false)}}
        modalRender={(modal) => (
          <Draggable
            disabled={disabledEdit}
            bounds={bounds}
            nodeRef={draggleRef}
            onStart={(event, uiData) => onStart(event, uiData)}
          >
            <div ref={draggleRef}>{modal}</div>
          </Draggable>
        )}
      >
        <Form layout="inline"
          onFinish={onStudentEdit}
          form={formEdit}>
          <Form.Item label="Группа" name = "groupId" className="input-inline">
            <Select
              allowClear
              options={groupOptions}
              style = {{width: '180px'}}
            >
            </Select>
          </Form.Item>
          <Form.Item name = "studentId" hidden>
          </Form.Item>
        <Form.Item label="Фамилия" name = "lastName" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Имя" name = "firstName" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Отчество" name = "midname" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="E-mail" name = "email" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Пароль" name = "password" className="input-inline" rules={[{ required: true, message: "Введите пароль"}]}>
            <Input />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType='submit'>Сохранить</Button>
          </Form.Item>
          </Form>
      </Modal>
        </>
    );
}