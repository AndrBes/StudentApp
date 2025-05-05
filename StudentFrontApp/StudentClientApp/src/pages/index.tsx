import { Button, Form, Popconfirm, Select, Table, TableProps } from 'antd';
import { request, useModel } from '@umijs/max';
import { useEffect, useState } from 'react';
import Input from 'antd/es/input/Input';
import ModalStudentAdd from '@/components/modals/ModalStudentAdd';
import ModalStudentEdit from '@/components/modals/ModalStudentEdit';

export default function HomePage() {
  const { initialState } = useModel('@@initialState');
  const { setModalOpenState,
      dataSource,
      setDataSourceState,
      loadGroups,
      onEdit,
      groupOptions } = useModel('useStudentModel')


  const columns: TableProps<any>['columns'] = [
    {
      title: 'Id',
      dataIndex: 'studentId'
    },
    {
      title: 'Фамилия',
      dataIndex: 'lastName'
    },
    {
      title: 'Имя',
      dataIndex: 'firstName'
    },
    {
      title: 'Отчество',
      dataIndex: 'midname'
    },
    {
      title: 'E-mail',
      dataIndex: 'email'
    },
    {
      title: 'Группа',
      dataIndex: 'groupName'
    },
    // <DeleteOutlined />
    {
      title: 'Действия',
      dataIndex: 'action',
      render: (value, record, index) => <>
      <Popconfirm
        title="Удалить студента"
        description="Вы точно хотите удалить студента?"
        onConfirm={() => onRemove(record.studentId) }
        okText="Да"
        cancelText="Нет"
      >
              <a>Удалить</a>
      </Popconfirm>
      <Button onClick={() => onEdit(record.studentId)}>Редактировать</Button>
      </>
    }
  ]

  // const [groupOptions, setGroupOptions] = useState<any[]>();

  useEffect(() => {

    onSearch({})
    loadGroups();

    //localStorage.setItem('super_key', 'super_value')
    //localStorage.removeItem('super_key')

  }, [])

  const onSearch = (data: any) => {
    console.log(data)
    request('/api/Student/GetAll', { data: data, method: 'POST' }).then((result: any[]) => {
      setDataSourceState(result)
    });
  }
  const onRemove = (studentId: number) => {
    console.log('Удаляю студента с id = ' + studentId)
    request(`/api/Student/Delete?id=${studentId}`, { method: 'DELETE'}).then((result: any[]) => {
      const newDataSource = dataSource?.filter(item => item.studentId != studentId)
      setDataSourceState(newDataSource)
    });
  }

  return (
    <div>
          <div>{initialState?.login ? `Вы авторизованы как ${initialState.login}` : '123'}</div>
    <>
      <Button onClick={() => setModalOpenState(true)}>Добавить студента</Button>
    <ModalStudentEdit/>
    <ModalStudentAdd/>
    </>
    <Form
          layout="inline"
          onFinish={onSearch}
          // form={form}
          //initialValues={{ layout: formLayout }}
          //onValuesChange={onFormLayoutChange}
        >
          <Form.Item label="Группа" name = "groupId" className="input-inline">
            <Select
              allowClear
              options={groupOptions}
              style = {{width: '180px'}}
            >
            </Select>
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
          <Form.Item>
            <Button type="primary" htmlType='submit'>Найти</Button>
          </Form.Item>
      </Form>
        <Table rowKey="studentId" dataSource={dataSource} columns={columns}></Table>
    </div>
  );
}
