**No Pull Requests Please!** for now. Just my personal hobby, You are free to fork and play.
Bugs are welcome!

# Advanced Algorithms

Various important computer science algorithms generically implemented in C#

## Data Structures

### List

* Array List (dynamic array) ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/List/AsArrayList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Lists/ArrayList_Tests.cs))
* Skip List ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/List/AsSkipList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Lists/SkipList_Tests.cs))

### Hash Set

* HashSet (using [Separate Chaining](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/AsSeparateChainingHashSet.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/AsOpenAddressHashSet.cs)) ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/AsHashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/HashSet/HashSet_Tests.cs))
* Tree HashSet ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/AsTreeHashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/HashSet/TreeHashSet_Tests.cs))

### Hash Dictionaries

* Dictionary (using [Separate Chaining](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/AsSeparateChainingDictionary.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/AsOpenAddressDictionary.cs)) ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/AsDictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Dictionary/Dictionary_Tests.cs))
* Tree Dictionary ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/AsTreeDictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Dictionary/TreeDictionary_Tests.cs))

### Stack

* Stack ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Stack/AsStack.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Stack_Tests.cs))

### Queue

* Queue ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Queue/AsQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Queue_Tests.cs))

#### Priority Queue

* Min Priority Queue ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Queue/PriorityQueue/Min/AsPriorityQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/PriorityQueue_Tests.cs))
 
### Linked List

* Singly linked list ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/LinkedList/AsSinglyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/LinkedList/SinglyLinkedList_Tests.cs))
* Doubly linked list ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/LinkedList/AsDoublyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/LinkedList/DoublyLinkedList_Tests.cs))
* Circular linked list ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/LinkedList/AsCircularLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/LinkedList/CircularLinkedList_Tests.cs))

### Heap

#### Min

* Binary Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/AsBMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/BMinHeap_Tests.cs))
* d-ary Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/Asd-aryMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/D-aryHeap_Tests.cs))
* Binomial Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/AsBinomialMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/BinomialHeap_Tests.cs))
* Fibornacci Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/AsFibornacciMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/FibornacciHeap_Tests.cs))
* Pairing Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/AsPairingMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/PairingHeap_Tests.cs))

Note: Among the implementations here in practice, with the exclusion of DecrementKey operation regular Binary Heap & d-ary Heap outperforms other in theory superiors. Of course because it don't have pointer juggling nonsense and hey arrays are faster!

### Tree

* Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/Tree_Tests.cs))
* Binary Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsBinaryTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/BinaryTree_Tests.cs))

#### Binary Search Trees

* Binary Search Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsBST.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/BST_Tests.cs))
* AVL Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsAvlTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/AVLTree_Tests.cs))
* Red Black Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsRedBlackTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/RedBlackTree_Tests.cs))
* Splay Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsSplayTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/SplayTree_Tests.cs))
* Treap Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsTreapTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/TreapTree_Tests.cs))

#### B Trees

* B Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsBTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/BTree_Tests.cs))
* B+ Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsB%2BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/B%2BTree_Tests.cs))

#### Queryable Trees

* Multi-dimensional Interval Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsIntervalTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/IntervalTree_Tests.cs)) using multi-level Red-black tree
* Multi-dimensional Kd Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsK_DTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/KdTree_Tests.cs)) for range and nearest neigbour queries
* Multi-dimensional Range Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsRangeTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/AsRangeTreeTests.cs))
* Segment Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsSegmentTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/SegmentTree_Tests.cs))
* Binary Indexed Tree (Fenwick tree) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsFenwickTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/FenwickTree_Tests.cs))

#### LookUp Trees

* Prefix Tree (Trie) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsTrie.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/Trie_Tests.cs))
* Suffix Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsSuffixTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/SuffixTree_Tests.cs))
* Ternary Search Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsTernarySearchTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/TernarySearchTree_Tests.cs))

#### Parse Trees

* Expression Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/AsExpressionTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/ExpressionTree_Tests.cs))

#### Set

* Disjoint Set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Set/AsDisJointSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Set/DisJointSet_Tests.cs))

### Graph

#### Adjacency List
* DiGraph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/AsDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/DiGraph_Tests.cs))
* Graph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/AsGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/Graph_Tests.cs))
* Weighted DiGraph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/AsWeightedDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/WeightedDiGraph_Tests.cs))
* Weighted Graph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/AsWeightedGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/WeightedGraph_Tests.cs))

# Algorithms

## Graph Algorithms

### Articulation Points

* Tarjans Articulation Points Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ArticulationPoint/TarjansArticulationFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ArticulationPoints/TarjansArticulation_Tests.cs))

### Bridges

* Tarjans Bridge Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Bridge/TarjansBridgeFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Bridge/TarjansBridge_Tests.cs))

### Connectivity

* Kosarajus Strongly Connected Component Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Connectivity/KosarajuStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Connectivity/KosarajuStronglyConnected_Tests.cs))
* Tarjans Strongly Connected Component Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Connectivity/TarjansStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Connectivity/TarjansStronglyConnected_Tests.cs))
* Tarjans BiConnected Graph Tester ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Connectivity/TarjansBiConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Connectivity/TarjansBiConnected_Tests.cs))

### Coloring

* M-Coloring ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Coloring/MColorer.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Coloring/MColoring_Tests.cs))

### Cover

* Min Vertex Cover ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Cover/MinVertexCover.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Cover/MinVertexCover.cs))

### Max Flow

* Ford-Fulkerson algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Flow/FordFulkerson.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Flow/FordFulkerson_Tests.cs))
* Edmonds Karp's improvement ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Flow/EdmondsKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Flow/EdmondsKarp_Tests.cs)) on Ford-Fulkerson algorithm
* Push Relabel algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Flow/PushRelabel.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Flow/PushRelabel_Tests.cs))

### Shortest Path

* Bellman-Ford algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Bellman-Ford.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/BellmanFord_Tests.cs))
* Dijikstras algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Dijikstra.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/Dijikstras_Tests.cs)) using Fibornacci Heap.
* Floyd-Warshall algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Floyd-Warshall.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/FloydWarshall_Tests.cs))
* Johnson's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Johnsons.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/Johnson_Tests.cs))

### Matching

* Max Bipartite Matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Matching/BiPartiteMatching.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Matching/BiPartiteMatching_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm 
* Hopcroft Karp algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Matching/HopcroftKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Matching/HopcroftKarp_Tests.cs))

### Cut

* Minimum Cut ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Cut/MinimumCut.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Cut/MinCut_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm

### Search

* Depth First ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Search/DepthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Search/DepthFirst_Tests.cs))
* Breadth First ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Search/BreadthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Search/BreadthFirst_Tests.cs))
* Bi-Directional ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Search/BiDirectional.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Search/BiDirectional_Tests.cs))

### Topological Sort

* Depth First Method ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/TopologicalSort/DepthFirstTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/TopologicalSort/DepthFirstTopSort_Tests.cs))
* Kahn's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/TopologicalSort/KahnTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/TopologicalSort/KahnTopSort_Tests.cs))

### Minimum Spanning Tree

* Kruskals algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/MinimumSpanningTree/Kruskals.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/MinimumSpanningTree/Kruskals_Test.cs)) using Merge Sort and Disjoint Set
* Prims algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/MinimumSpanningTree/Prims.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/MinimumSpanningTree/Prims_Test.cs)) using Fibornacci Heap

## String

* Permutation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Permutation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Permutate.cs))
* Manacher's algorithm for linear time longest Palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/ManachersPalindrome.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Manachers_Tests.cs))

### Pattern Matching

* Rabin-Karp string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Search/RabinKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Search/RabinKarp_Tests.cs))
* Knuth–Morris–Pratt (KMP) string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Search/KMP.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Search/KMP_Tests.cs))
* Z algorithm for string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Search/ZAlgorithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Search/Z_Tests.cs))

## Compression

* Huffman Coding ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Compression/HuffmanCoding.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Compression/HuffmanCoding_Tests.cs)) using Fibornacci Min Heap

## Sorting

* Bubble Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/BubbleSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/BubbleSort_Tests.cs))
* Insertion Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/InsertionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/InsertionSort_Tests.cs))
* Selection Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/SelectionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/SelectionSort_Tests.cs))
* Shell Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/ShellSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/ShellSort_Tests.cs))
* Tree Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/TreeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/TreeSort_Tests.cs))
* Quick Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/QuickSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/QuickSort_Tests.cs))
* Heap Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/HeapSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/HeapSort_Tests.cs))
* Merge Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/MergeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/MergeSort_Tests.cs))
* Bucket Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/BucketSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/BucketSort_Tests.cs))
* Radix Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/RadixSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/RadixSort_Tests.cs))
* Counting Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/CountingSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/CountingSort_Tests.cs))

Note: On a decent desktop, in given implementations here for +ive random input integers, the clear winner is counting sort (~0.6 seconds to sort 1 million integers) followed by Radix Sort. Merge Sort, Heap Sort, Quick Sort & Bucket Sort are all winners for +ive & -ive random integer inputs with (~1 seconds to sort 1 million integers). Tree sort has pointer juggling overhead on backing Red-Black Tree, so performs 8 times slower than Merge Sort in practice. Bubble Sort, Insertion Sort, Selection Sort & Shell Sort are among the worst for random input as you might have guessed.

## Numerical Methods

* kth Largest/Smallest ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/KthSmallest.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/KthSmallest_Tests.cs))
* Median of stream of numbers ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/MedianStream.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/MedianStream_Tests.cs))
* Check Primality
* Generate Primes using Sieve of Eratosthenes
* Fast Exponentiation

## Classic Dynamic Programming Problems

* Coin Change Problem ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DynamicProgramming/CoinChangeProblems.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/CoinChangeProblems_Tests.cs))
* Tower Of Hanoi ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DynamicProgramming/TowerOfHanoi.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/TowerOfHanoi_Tests.cs))
* Knapsack problem ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DynamicProgramming/KnackSackProblems.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/KnackSackProblems_Tests.cs))
* Fibonacci Number Generator
* Balanced Partition
* Count Bool Parenthesization
* Count Decoding
* Dice Throw
* Distinct Binary Strings
* Longest Common Sub Sequence
* Longest Increasing Sub Sequence
* Longest Palindrome
* Maximum Sub Matrix
* Maximum Value Sub Sequence
* Min Edit Distance
* Min Array Jumps
* Optimal Game Strategy
* Palindrome Min Cut
* Print Max A's
* Shortest Palindrome
* Sub Set Sum
* Text Justification
* Two Person City Traversal
* Unique Binary Search Tree
* Word Break problem

