Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports OrdenaminetoFormsVB.Unit
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RunGame()

    End Sub




    Private sortingAlgorithmComboBox As ComboBox
    Private armyListBox As ListBox
    Public Sub New()
        ' Configuración de la ventana principal
        Text = "Army Management"
        Size = New System.Drawing.Size(400, 300)

        ' Configuración del ComboBox para seleccionar el algoritmo de ordenamiento
        sortingAlgorithmComboBox = New ComboBox With {
.Location = New System.Drawing.Point(10, 10),
.Size = New System.Drawing.Size(200, 25),
                .DropDownStyle = ComboBoxStyle.DropDownList
            }

        ' Agrega los algoritmos de ordenamiento al ComboBox
        sortingAlgorithmComboBox.Items.AddRange(GetSortingAlgorithmNames())

        ' Configuración de la ListBox para mostrar la lista de unidades
        armyListBox = New ListBox With {
.Location = New System.Drawing.Point(10, 50),
.Size = New System.Drawing.Size(300, 200)
}

        ' Configuración del evento de cambio de selección en el ComboBox
        AddHandler sortingAlgorithmComboBox.SelectedIndexChanged, AddressOf RunGame

        ' Agrega los controles a la ventana
        Controls.Add(sortingAlgorithmComboBox)
        Controls.Add(armyListBox)

        ' Asigna el evento Load del formulario
        AddHandler Load, AddressOf Form1_Load
    End Sub
    Private Sub RunGame()
        Dim army As List(Of Unit) = GenerateArmy()

        ' Obtiene el algoritmo de ordenamiento seleccionado
        Dim sortingAlgorithm As ISortAlgorithm(Of Unit) = GetSortingAlgorithm(sortingAlgorithmComboBox.SelectedIndex + 1)

        ' Ordena el ejército si se seleccionó un algoritmo válido
        If sortingAlgorithm IsNot Nothing Then
            army = sortingAlgorithm.Sort(army, Function(x, y) Comparer(Of Unit).Default.Compare(x, y))

            ' Muestra el ejército ordenado en la ListBox
            armyListBox.Items.Clear()
            For Each unit In army
                armyListBox.Items.Add(unit)
            Next
        End If
    End Sub

    Private Function GetSortingAlgorithmNames() As String()
        ' Retorna los nombres de los algoritmos de ordenamiento
        Return {
"Shell Sort", "Selection Sort", "HeapSort", "QuickSort", "BubbleSort",
                "CocktailSort", "InsertionSort", "BucketSort", "CountingSort", "MergeSort",
                "BinaryTreeSort", "RadixSort", "GnomeSort", "NaturalMergeSort", "StraightMergeSort"
}
    End Function
    Private Function GetSortingAlgorithm(choice As Integer) As ISortAlgorithm(Of Unit)
        ' Retorna la instancia del algoritmo de ordenamiento seleccionado
        Select Case choice
            Case 1
                Return New ShellSort(Of Unit)()
            Case 2
                Return New SelectionSort(Of Unit)()
            Case 3
                Return New HeapSort(Of Unit)()
            Case 4
                Return New QuickSort(Of Unit)()
            Case 5
                Return New BubbleSort(Of Unit)()
            Case 6
                Return New CocktailSort(Of Unit)()
            Case 7
                Return New InsertionSort(Of Unit)()
            Case 8
                Return New BucketSort(Of Unit)()
            Case 9
                Return New CountingSort(Of Unit)()
            Case 10
                Return New MergeSort(Of Unit)()
            Case 11
                Return New BinaryTreeSort(Of Unit)()
            Case 12
                Return New RadixSort(Of Unit)()
            Case 13
                Return New GnomeSort(Of Unit)()
            Case 14
                Return New NaturalMergeSort(Of Unit)()
            Case 15
                Return New StraightMergeSort(Of Unit)()
            Case Else
                Return Nothing
        End Select
    End Function

    Private Function GenerateArmy() As List(Of Unit)
        ' Genera una lista de unidades aleatorias
        Dim random As New Random()
        Dim army As New List(Of Unit)()

        For i As Integer = 0 To 4
            army.Add(New Unit With {
                    .Name = $"Unit{i + 1}",
                    .Level = random.Next(1, 10),
                    .AttackPower = random.Next(10, 30),
                    .Speed = random.Next(5, 20)
                })
        Next

        Return army
    End Function
End Class
Interface ISortAlgorithm(Of T As IComparable(Of T))
    Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T)
End Interface
Class Unit
    Implements IComparable(Of Unit)
    Public Property Name As String
    Public Property Level As Integer
    Public Property AttackPower As Integer
    Public Property Speed As Integer

    Public Function CompareTo(other As Unit) As Integer Implements IComparable(Of Unit).CompareTo
        ' Implementa la lógica de comparación aquí
        ' Puedes comparar por nivel, velocidad, poder de ataque, etc.
        ' Ejemplo: Return Me.Level.CompareTo(other.Level)

        ' Por ejemplo, comparar por nivel y luego por velocidad si los niveles son iguales
        Dim levelComparison As Integer = Me.Level.CompareTo(other.Level)
        If levelComparison <> 0 Then
            Return levelComparison
        End If

        Return Me.Speed.CompareTo(other.Speed)
    End Function
    Public Overrides Function ToString() As String
        Return $"{Name} (Level: {Level}, Attack: {AttackPower}, Speed: {Speed})"
    End Function
    Class ShellSort(Of T As IComparable(Of T))
        Implements ISortAlgorithm(Of T)

        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count
            Dim gap As Integer = n \ 2
            While gap > 0
                For i As Integer = gap To n - 1
                    Dim temp As T = input(i)
                    Dim j As Integer = i

                    While j >= gap AndAlso comparison(input(j - gap), temp) > 0
                        input(j) = input(j - gap)
                        j -= gap
                    End While

                    input(j) = temp
                Next

                gap \= 2
            End While

            Return input
        End Function
    End Class

    Class SelectionSort(Of T As IComparable(Of T))
        Implements ISortAlgorithm(Of T)

        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count

            For i As Integer = 0 To n - 2
                Dim minIndex As Integer = i

                For j As Integer = i + 1 To n - 1
                    If comparison(input(j), input(minIndex)) < 0 Then
                        minIndex = j
                    End If
                Next

                ' Swap elements
                Dim temp As T = input(i)
                input(i) = input(minIndex)
                input(minIndex) = temp
            Next

            Return input
        End Function
    End Class

    Class NaturalMergeSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim runs As List(Of List(Of T)) = IdentifyRuns(input, comparison)

            While runs.Count > 1
                Dim result As New List(Of T)()
                Dim i As Integer = 0

                While i < runs.Count - 1
                    result.AddRange(Merge(runs(i), runs(i + 1), comparison))
                    i += 2
                End While

                If i = runs.Count - 1 Then
                    result.AddRange(runs(i))
                End If

                runs = IdentifyRuns(result, comparison)
            End While

            Return runs(0)
        End Function

        Private Function IdentifyRuns(input As List(Of T), comparison As Comparison(Of T)) As List(Of List(Of T))
            Dim runs As New List(Of List(Of T))()
            Dim currentRun As New List(Of T) From {input(0)}

            For i As Integer = 1 To input.Count - 1
                If comparison(input(i), input(i - 1)) >= 0 Then
                    currentRun.Add(input(i))
                Else
                    runs.Add(currentRun)
                    currentRun = New List(Of T) From {input(i)}
                End If
            Next

            runs.Add(currentRun)
            Return runs
        End Function

        Private Function Merge(left As List(Of T), right As List(Of T), comparison As Comparison(Of T)) As List(Of T)
            Dim result As New List(Of T)()
            Dim leftIndex As Integer = 0, rightIndex As Integer = 0

            While leftIndex < left.Count AndAlso rightIndex < right.Count
                If comparison(left(leftIndex), right(rightIndex)) <= 0 Then
                    result.Add(left(leftIndex))
                    leftIndex += 1
                Else
                    result.Add(right(rightIndex))
                    rightIndex += 1
                End If
            End While

            While leftIndex < left.Count
                result.Add(left(leftIndex))
                leftIndex += 1
            End While

            While rightIndex < right.Count
                result.Add(right(rightIndex))
                rightIndex += 1
            End While

            Return result
        End Function
    End Class

    Class StraightMergeSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Return MergeSort(input, comparison)
        End Function

        Private Function MergeSort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T)
            If input.Count <= 1 Then
                Return input
            End If

            Dim middle As Integer = input.Count \ 2
            Dim left As List(Of T) = input.GetRange(0, middle)
            Dim right As List(Of T) = input.GetRange(middle, input.Count - middle)

            left = MergeSort(left, comparison)
            right = MergeSort(right, comparison)

            Return Merge(left, right, comparison)
        End Function

        Private Function Merge(left As List(Of T), right As List(Of T), comparison As Comparison(Of T)) As List(Of T)
            Dim result As New List(Of T)()
            Dim leftIndex As Integer = 0, rightIndex As Integer = 0

            While leftIndex < left.Count AndAlso rightIndex < right.Count
                If comparison(left(leftIndex), right(rightIndex)) <= 0 Then
                    result.Add(left(leftIndex))
                    leftIndex += 1
                Else
                    result.Add(right(rightIndex))
                    rightIndex += 1
                End If
            End While

            While leftIndex < left.Count
                result.Add(left(leftIndex))
                leftIndex += 1
            End While

            While rightIndex < right.Count
                result.Add(right(rightIndex))
                rightIndex += 1
            End While

            Return result
        End Function
    End Class

    Class HeapSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count

            For i As Integer = n \ 2 - 1 To 0 Step -1
                Heapify(input, n, i, comparison)
            Next

            For i As Integer = n - 1 To 0 Step -1
                Dim temp As T = input(0)
                input(0) = input(i)
                input(i) = temp

                Heapify(input, i, 0, comparison)
            Next

            Return input
        End Function

        Private Sub Heapify(arr As List(Of T), n As Integer, i As Integer, comparison As Comparison(Of T))
            Dim largest As Integer = i
            Dim left As Integer = 2 * i + 1
            Dim right As Integer = 2 * i + 2

            If left < n AndAlso comparison(arr(left), arr(largest)) > 0 Then
                largest = left
            End If

            If right < n AndAlso comparison(arr(right), arr(largest)) > 0 Then
                largest = right
            End If

            If largest <> i Then
                Dim swap As T = arr(i)
                arr(i) = arr(largest)
                arr(largest) = swap

                Heapify(arr, n, largest, comparison)
            End If
        End Sub
    End Class

    Class QuickSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            QuickSortRecursive(input, 0, input.Count - 1, comparison)
            Return input
        End Function

        Private Sub QuickSortRecursive(arr As List(Of T), low As Integer, high As Integer, comparison As Comparison(Of T))
            If low < high Then
                Dim partitionIndex As Integer = Partition(arr, low, high, comparison)

                QuickSortRecursive(arr, low, partitionIndex - 1, comparison)
                QuickSortRecursive(arr, partitionIndex + 1, high, comparison)
            End If
        End Sub

        Private Function Partition(arr As List(Of T), low As Integer, high As Integer, comparison As Comparison(Of T)) As Integer
            Dim pivot As T = arr(high)
            Dim i As Integer = low - 1

            For j As Integer = low To high - 1
                If comparison(arr(j), pivot) < 0 Then
                    i += 1
                    Dim temp As T = arr(i)
                    arr(i) = arr(j)
                    arr(j) = temp
                End If
            Next

            Dim temp2 As T = arr(i + 1)
            arr(i + 1) = arr(high)
            arr(high) = temp2

            Return i + 1
        End Function
    End Class
    Class BubbleSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count

            For i As Integer = 0 To n - 2
                For j As Integer = 0 To n - i - 2
                    If comparison(input(j), input(j + 1)) > 0 Then
                        Dim temp As T = input(j)
                        input(j) = input(j + 1)
                        input(j + 1) = temp
                    End If
                Next
            Next

            Return input
        End Function
    End Class

    Class CocktailSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count
            Dim swapped As Boolean

            Do
                swapped = False

                For i As Integer = 0 To n - 2
                    If comparison(input(i), input(i + 1)) > 0 Then
                        Dim temp As T = input(i)
                        input(i) = input(i + 1)
                        input(i + 1) = temp
                        swapped = True
                    End If
                Next

                If Not swapped Then
                    Exit Do
                End If

                swapped = False

                For i As Integer = n - 2 To 0 Step -1
                    If comparison(input(i), input(i + 1)) > 0 Then
                        Dim temp As T = input(i)
                        input(i) = input(i + 1)
                        input(i + 1) = temp
                        swapped = True
                    End If
                Next

            Loop While swapped

            Return input
        End Function
    End Class
    Class InsertionSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count

            For i As Integer = 1 To n - 1
                Dim key As T = input(i)
                Dim j As Integer = i - 1

                While j >= 0 AndAlso comparison(input(j), key) > 0
                    input(j + 1) = input(j)
                    j = j - 1
                End While

                input(j + 1) = key
            Next

            Return input
        End Function
    End Class

    Class BucketSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            If input.Count = 0 Then
                Return input
            End If

            ' Encuentra el valor máximo y mínimo en la lista
            Dim minValue As T = input(0)
            Dim maxValue As T = input(0)

            For Each item In input
                If comparison(item, minValue) < 0 Then
                    minValue = item
                End If

                If comparison(item, maxValue) > 0 Then
                    maxValue = item
                End If
            Next

            ' Inicializa los baldes
            Dim buckets As New List(Of List(Of T))

            ' Crea los baldes
            For i As Integer = 0 To input.Count - 1
                buckets.Add(New List(Of T)())
            Next

            ' Distribuye los elementos en los baldes
            For Each item In input
                Dim bucketIndex As Integer = input.IndexOf(item)
                buckets(bucketIndex).Add(item)
            Next

            ' Ordena cada balde e inserta los elementos ordenados en la lista final
            Dim sortedList As New List(Of T)
            For Each bucket In buckets
                If bucket.Count > 0 Then
                    bucket.Sort(comparison)
                    sortedList.AddRange(bucket)
                End If
            Next

            Return sortedList
        End Function
    End Class
    Class CountingSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            ' Encuentra el valor máximo en la lista
            Dim maxVal As T = input(0)
            For Each item In input
                If comparison(item, maxVal) > 0 Then
                    maxVal = item
                End If
            Next

            ' Crea un diccionario para contar la frecuencia de cada elemento
            Dim count As New Dictionary(Of T, Integer)()

            ' Llena el diccionario de conteo
            For Each item In input
                If count.ContainsKey(item) Then
                    count(item) += 1
                Else
                    count(item) = 1
                End If
            Next

            ' Reconstruye el array ordenado utilizando el diccionario de conteo
            Dim sortedList As New List(Of T)()
            For Each kvp In count
                For j As Integer = 0 To kvp.Value - 1
                    sortedList.Add(kvp.Key)
                Next
            Next

            Return sortedList
        End Function
    End Class

    Class MergeSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            If input.Count <= 1 Then
                Return input
            End If

            Dim middle As Integer = input.Count \ 2
            Dim left As List(Of T) = input.GetRange(0, middle)
            Dim right As List(Of T) = input.GetRange(middle, input.Count - middle)

            left = Sort(left, comparison)
            right = Sort(right, comparison)

            Return Merge(left, right, comparison)
        End Function

        Private Function Merge(left As List(Of T), right As List(Of T), comparison As Comparison(Of T)) As List(Of T)
            Dim result As New List(Of T)()
            Dim leftIndex As Integer = 0, rightIndex As Integer = 0

            While leftIndex < left.Count AndAlso rightIndex < right.Count
                If comparison(left(leftIndex), right(rightIndex)) <= 0 Then
                    result.Add(left(leftIndex))
                    leftIndex += 1
                Else
                    result.Add(right(rightIndex))
                    rightIndex += 1
                End If
            End While

            While leftIndex < left.Count
                result.Add(left(leftIndex))
                leftIndex += 1
            End While

            While rightIndex < right.Count
                result.Add(right(rightIndex))
                rightIndex += 1
            End While

            Return result
        End Function
    End Class
    Class BinaryTreeSort(Of TNode As IComparable(Of TNode)) : Implements ISortAlgorithm(Of TNode)
        Private root As Node

        Public Function Sort(input As List(Of TNode), comparison As Comparison(Of TNode)) As List(Of TNode) Implements ISortAlgorithm(Of TNode).Sort
            For Each item In input
                root = InsertRec(root, item)
            Next

            Dim sortedList As New List(Of TNode)()
            InOrderTraversalRec(root, Sub(value) sortedList.Add(value))

            Return sortedList
        End Function

        Private Function InsertRec(root As Node, value As TNode) As Node
            If root Is Nothing Then
                Return New Node(value)
            End If

            If value.CompareTo(root.Value) < 0 Then
                root.Left = InsertRec(root.Left, value)
            ElseIf value.CompareTo(root.Value) > 0 Then
                root.Right = InsertRec(root.Right, value)
            End If

            Return root
        End Function

        Private Sub InOrderTraversalRec(root As Node, action As Action(Of TNode))
            If root IsNot Nothing Then
                InOrderTraversalRec(root.Left, action)
                action(root.Value)
                InOrderTraversalRec(root.Right, action)
            End If
        End Sub

        Private Class Node
            Public Property Value As TNode
            Public Property Left As Node
            Public Property Right As Node

            Public Sub New(value As TNode)
                Me.Value = value
            End Sub
        End Class
    End Class
    Class RadixSort(Of T As Unit) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            ' Obtiene la longitud máxima del nivel para determinar la cantidad de pasadas
            Dim maxLevelLength As Integer = GetMaxLevelLength(input)

            ' Aplica el Radix Sort por cada posición en el nivel
            For digitPlace As Integer = 1 To maxLevelLength
                CountingSortByLevel(input, digitPlace, comparison)
            Next

            Return input
        End Function

        Private Sub CountingSortByLevel(input As List(Of T), digitPlace As Integer, comparison As Comparison(Of T))
            Dim n As Integer = input.Count

            ' Inicializa el array de conteo
            Dim count As New Dictionary(Of Integer, List(Of T))()

            ' Llena el array de conteo
            For Each item In input
                Dim level As Integer = GetLevel(item, digitPlace)
                If Not count.ContainsKey(level) Then
                    count(level) = New List(Of T)()
                End If
                count(level).Add(item)
            Next

            ' Reconstruye el array ordenado utilizando el array de conteo
            Dim sortedList As New List(Of T)()
            For Each kvp In count
                kvp.Value.Sort(comparison)
                sortedList.AddRange(kvp.Value)
            Next

            ' Actualiza la lista original con la lista ordenada
            For i As Integer = 0 To n - 1
                input(i) = sortedList(i)
            Next
        End Sub

        Private Function GetMaxLevelLength(input As List(Of T)) As Integer
            Dim maxLevelLength As Integer = 0

            For Each item In input
                Dim levelLength As Integer = GetLevelLength(item)
                If levelLength > maxLevelLength Then
                    maxLevelLength = levelLength
                End If
            Next

            Return maxLevelLength
        End Function

        Private Function GetLevelLength(item As T) As Integer
            Return item.ToString().Length
        End Function

        Private Function GetLevel(item As T, digitPlace As Integer) As Integer
            Dim level As Integer = 0

            If TypeOf item Is Unit Then
                ' Asegúrate de que la propiedad que estás utilizando existe en la clase Unit
                Dim unit As Unit = DirectCast(item, Unit)
                level = unit.AttackPower

                ' Convierte el nivel a una cadena
                Dim levelString As String = level.ToString()

                ' Asegúrate de que la posición en el nivel sea válida
                If digitPlace <= levelString.Length Then
                    ' Obtiene el dígito en la posición específica
                    Return Integer.Parse(levelString(levelString.Length - digitPlace).ToString())
                End If
            End If

            Return 0
        End Function
    End Class
    Class GnomeSort(Of T As IComparable(Of T)) : Implements ISortAlgorithm(Of T)
        Public Function Sort(input As List(Of T), comparison As Comparison(Of T)) As List(Of T) Implements ISortAlgorithm(Of T).Sort
            Dim n As Integer = input.Count
            Dim index As Integer = 0

            While index < n
                If index = 0 Then
                    index += 1
                End If

                If comparison(input(index), input(index - 1)) >= 0 Then
                    index += 1
                Else
                    Swap(input, index, index - 1)
                    index -= 1
                End If
            End While

            Return input
        End Function

        Private Sub Swap(input As List(Of T), i As Integer, j As Integer)
            Dim temp As T = input(i)
            input(i) = input(j)
            input(j) = temp
        End Sub
    End Class
End Class

