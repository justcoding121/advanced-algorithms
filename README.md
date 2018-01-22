# Advanced Algorithms

Various important computer science algorithms generically implemented in C#

<a href="https://ci.appveyor.com/project/justcoding121/advanced-algorithms">![Build Status](https://ci.appveyor.com/api/projects/status/9xpcp4m87max2066?svg=true)</a>

Install by [nuget](https://www.nuget.org/packages/Advanced.Algorithms)

For beta releases on [beta branch](https://github.com/justcoding121/Advanced-Algorithms/tree/beta)

    Install-Package Advanced.Algorithms -Pre

Not a stable release yet.

Supports

 * .Net Standard 1.0 or above
 * .Net Framework 4.0 or above
 
## Data Structures

### List

- [X] Array List (dynamic array) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/List/ArrayList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Lists/ArrayList_Tests.cs))
- [X] Skip List ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/List/SkipList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Lists/SkipList_Tests.cs))

### Hash Set

- [X] HashSet (using [Separate Chaining](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/HashSet/SeparateChainingHashSet.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/HashSet/OpenAddressHashSet.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/HashSet/HashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/HashSet/HashSet_Tests.cs))
- [X] Tree HashSet ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/HashSet/TreeHashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/HashSet/TreeHashSet_Tests.cs))

### Hash Dictionaries

- [X] Dictionary (using [Separate Chaining](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Dictionary/SeparateChainingDictionary.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Dictionary/OpenAddressDictionary.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Dictionary/Dictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Dictionary/Dictionary_Tests.cs))
- [X] Tree Dictionary ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Dictionary/TreeDictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Dictionary/TreeDictionary_Tests.cs))

### Stack

- [X] Stack (using [Dynamic Array](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Stack/ArrayStack.cs) and optionally using [Singly Linked List](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Stack/LinkedListStack.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Stack/Stack.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Stack_Tests.cs))

### Queue

- [X] Queue (using [Dynamic Array](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Queues/ArrayQueue.cs) and optionally using [Doubly Linked List](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Queues/LinkedListQueue.cs)) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Queues/Queue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Queues/Queue_Tests.cs))

#### Priority Queue

- [X] Min Priority Queue ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MinPriorityQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue/MinPriorityQueue_Tests.cs))
- [X] Max Priority Queue ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Queues/PriorityQueue/MaxPriorityQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue/MaxPriorityQueue_Tests.cs))
 
### Linked List

- [X] Singly linked list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/LinkedList/SinglyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/LinkedList/SinglyLinkedList_Tests.cs))
- [X] Doubly linked list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/LinkedList/DoublyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/LinkedList/DoublyLinkedList_Tests.cs))
- [X] Circular linked list ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/LinkedList/CircularLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/LinkedList/CircularLinkedList_Tests.cs))

### Heap

#### Min

- [X] Binary Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Min/BMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Min/BMinHeap_Tests.cs))
- [X] d-ary Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Min/d-aryMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Min/D-aryMinHeap_Tests.cs))
- [X] Binomial Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Min/BinomialMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Min/BinomialMinHeap_Tests.cs))
- [X] Fibornacci Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Min/FibornacciMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Min/FibornacciMinHeap_Tests.cs))
- [X] Pairing Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Min/PairingMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Min/PairingMinHeap_Tests.cs))

#### Max 

- [X] Binary Max Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Max/BMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Max/BMaxHeap_Tests.cs))
- [X] d-ary Max Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Max/d-aryMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Max/D-aryMaxHeap_Tests.cs))
- [X] Binomial Max Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Max/BinomialMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Max/BinomialMaxHeap_Tests.cs))
- [X] Fibornacci Max Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Max/FibornacciMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Max/FibornacciMaxHeap_Tests.cs))
- [X] Pairing Max Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Heap/Max/PairingMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Heap/Max/PairingMaxHeap_Tests.cs))

Note: It is observed that among the implementations here in practice, with the exclusion of DecrementKey/IncrementKey operation regular Binary Heap & d-ary Heap outperforms other in theory superiors. Likely because it don't have pointer juggling overhead and hey arrays are faster!


### Tree

- [X] Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/Tree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/Tree_Tests.cs))
- [X] Binary Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/BinaryTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/BinaryTree_Tests.cs))

#### Binary Search Trees

- [X] Binary Search Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/BST.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/BST_Tests.cs))
- [X] AVL Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/AvlTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/AVLTree_Tests.cs))
- [X] Red Black Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/RedBlackTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/RedBlackTree_Tests.cs))
- [X] Splay Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/SplayTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/SplayTree_Tests.cs))
- [X] Treap Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/TreapTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/TreapTree_Tests.cs))

#### B Trees

- [X] B Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/BTree_Tests.cs))
- [X] B+ Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/B%2BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/B%2BTree_Tests.cs))

#### Queryable Trees

- [X] Multi-dimensional Interval Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/IntervalTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/IntervalTree_Tests.cs)) using nested Red-black tree
- [X] Multi-dimensional Kd Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/K_DTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/KdTree_Tests.cs)) for range and nearest neigbour queries
- [X] Multi-dimensional Range Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/RangeTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/RangeTreeTests.cs)) using nested Red-black tree
- [X] Segment Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/SegmentTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/SegmentTree_Tests.cs))
- [X] Binary Indexed Tree (Fenwick tree) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/FenwickTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/FenwickTree_Tests.cs))

#### LookUp Trees

- [X] Prefix Tree (Trie) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/Trie.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/Trie_Tests.cs))
- [X] Suffix Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/SuffixTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/SuffixTree_Tests.cs))
- [X] Ternary Search Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/TernarySearchTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/TernarySearchTree_Tests.cs))

#### Parse Trees

- [X] Expression Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Tree/ExpressionTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Tree/ExpressionTree_Tests.cs))

#### Set

- [X] Disjoint Set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Set/DisJointSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Set/DisJointSet_Tests.cs))
- [X] Sparse Set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Set/SparseSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Set/SparseSet_Tests.cs))
- [X] Bloom Filter ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Set/BloomFilter.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Set/BloomFilter_Tests.cs))

### Graph

#### Adjacency List

- [X] Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/Graph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/Graph_Tests.cs))
- [X] Weighted Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/WeightedGraph_Tests.cs))
- [X] DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/DiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/DiGraph_Tests.cs))
- [X] Weighted DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyList/WeightedDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyList/WeightedDiGraph_Tests.cs))

#### Adjacency Matrix

- [X] Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/Graph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/Graph_Tests.cs))
- [X] Weighted Graph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/WeightedGraph_Tests.cs))
- [X] DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/DiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/DiGraph_Tests.cs))
- [X] Weighted DiGraph ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DataStructures/Graph/AdjacencyMatrix/WeightedDiGraph_Tests.cs))


# Algorithms

## Graph Algorithms

### Articulation Points

- [X] Tarjan's Articulation Points Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/ArticulationPoint/TarjansArticulationFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/ArticulationPoints/TarjansArticulation_Tests.cs))

### Bridges

- [X] Tarjan's Bridge Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Bridge/TarjansBridgeFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Bridge/TarjansBridge_Tests.cs))

### Connectivity

- [X] Kosaraju's Strongly Connected Component Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Connectivity/KosarajuStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Connectivity/KosarajuStronglyConnected_Tests.cs))
- [X] Tarjan's Strongly Connected Component Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Connectivity/TarjansStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Connectivity/TarjansStronglyConnected_Tests.cs))
- [X] Tarjan's BiConnected Graph Tester ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Connectivity/TarjansBiConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Connectivity/TarjansBiConnected_Tests.cs))

### Coloring

- [X] M-Coloring ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Coloring/MColorer.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Coloring/MColoring_Tests.cs))

### Cover

- [X] Min Vertex Cover ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Cover/MinVertexCover.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Cover/MinVertexCover.cs))

### Max Flow

- [X] Ford-Fulkerson algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Flow/FordFulkerson.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Flow/FordFulkerson_Tests.cs))
- [X] Edmonds Karp's improvement ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Flow/EdmondsKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Flow/EdmondsKarp_Tests.cs)) on Ford-Fulkerson algorithm
- [X] Push Relabel algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Flow/PushRelabel.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Flow/PushRelabel_Tests.cs))

### Shortest Path

- [X] Bellman-Ford algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/ShortestPath/Bellman-Ford.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/ShortestPath/BellmanFord_Tests.cs))
- [X] Dijikstra's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/ShortestPath/Dijikstra.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/ShortestPath/Dijikstras_Tests.cs)) using Fibornacci Heap.
- [X] Floyd-Warshall algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/ShortestPath/Floyd-Warshall.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/ShortestPath/FloydWarshall_Tests.cs))
- [X] Johnson's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/ShortestPath/Johnsons.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/ShortestPath/Johnson_Tests.cs))
- [X] Travelling Salesman Problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/ShortestPath/TravellingSalesman.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/ShortestPath/TravellingSalesman_Tests.cs))

### Matching

- [X] Max Bipartite Matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Matching/BiPartiteMatching.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Matching/BiPartiteMatching_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm 
- [X] Max Bipartite Matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Matching/HopcroftKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Matching/HopcroftKarp_Tests.cs)) using Hopcroft Karp algorithm

### Cut

- [X] Minimum Cut ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Cut/MinimumCut.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Cut/MinCut_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm

### Search

- [X] Depth First ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Search/DepthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Search/DepthFirst_Tests.cs))
- [X] Breadth First ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Search/BreadthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Search/BreadthFirst_Tests.cs))
- [X] Bi-Directional ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/Search/BiDirectional.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/Search/BiDirectional_Tests.cs))

### Topological Sort

- [X] Depth First Method ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/TopologicalSort/DepthFirstTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/TopologicalSort/DepthFirstTopSort_Tests.cs))
- [X] Kahn's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/TopologicalSort/KahnTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/TopologicalSort/KahnTopSort_Tests.cs))

### Minimum Spanning Tree

- [X] Kruskal's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/MinimumSpanningTree/Kruskals.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/MinimumSpanningTree/Kruskals_Test.cs)) using Merge Sort and Disjoint Set
- [X] Prim's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/GraphAlgorithms/MinimumSpanningTree/Prims.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/GraphAlgorithms/MinimumSpanningTree/Prims_Test.cs))

## String

- [X] Manacher's algorithm for linear time longest Palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/String/ManachersPalindrome.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/String/Manachers_Tests.cs))

### Pattern Matching

- [X] Rabin-Karp string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/String/Search/RabinKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/String/Search/RabinKarp_Tests.cs))
- [X] Knuth–Morris–Pratt (KMP) string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/String/Search/KMP.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/String/Search/KMP_Tests.cs))
- [X] Z algorithm for string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/String/Search/ZAlgorithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/String/Search/Z_Tests.cs))

## Compression

- [X] Huffman Coding ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Compression/HuffmanCoding.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Compression/HuffmanCoding_Tests.cs)) 

## Sorting and Searching

- [X] Median of stream of numbers ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/NumericalMethods/MedianStream.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/NumericalMethods/MedianStream_Tests.cs))
- [X] Binary Search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Search/BinarySearch.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Search/BinarySearch_Tests.cs))
- [X] Search on almost sorted array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Search/SearchAlmostSorted.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Search/SearchAlmostSorted_Tests.cs))
- [X] Sort an almost sorted array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/SortAlmostSorted.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/SortAlmostSorted_Tests.cs))

### Sorting Algorithms

- [X] Bubble Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/BubbleSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/BubbleSort_Tests.cs))
- [X] Insertion Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/InsertionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/InsertionSort_Tests.cs))
- [X] Selection Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/SelectionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/SelectionSort_Tests.cs))
- [X] Shell Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/ShellSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/ShellSort_Tests.cs))
- [X] Tree Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/TreeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/TreeSort_Tests.cs))
- [X] Quick Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/QuickSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/QuickSort_Tests.cs))
- [X] Heap Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/HeapSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/HeapSort_Tests.cs))
- [X] Merge Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/MergeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/MergeSort_Tests.cs))
- [X] Bucket Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/BucketSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/BucketSort_Tests.cs))
- [X] Radix Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/RadixSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/RadixSort_Tests.cs))
- [X] Counting Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Sorting/CountingSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Sorting/CountingSort_Tests.cs))

Note: On a decent desktop, in given implementations here for +ive random input integers, the clear winner is counting sort (~0.1 seconds to sort 1 million integers) followed by Radix Sort (~0.4 seconds). Merge Sort, Heap Sort, Quick Sort & Bucket Sort are all winners for +ive & -ive random integer inputs. Tree sort has pointer juggling overhead on backing Red-Black Tree, so performs 8 times slower than Merge Sort in practice. Bubble Sort, Insertion Sort, Selection Sort & Shell Sort are among the worst for random input as observed from results.

## Combinatorics

- [X] Permutations ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Combinatorics/Permutation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Combinatorics/Permutation_Tests.cs))
- [X] Combinations ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Combinatorics/Combination.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Combinatorics/Combination_Tests.cs))
- [X] Subsets ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Combinatorics/Subset.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Combinatorics/Subset_Tests.cs))

## Distributed Systems

- [X] Circular Queue (Ring Buffer) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/DistributedSystems/CircularQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/DistributedSystems/CircularQueue_Tests.cs))

## Numerical Methods

- [X] kth Smallest ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/NumericalMethods/KthSmallest.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/NumericalMethods/KthSmallest_Tests.cs))
- [X] Check Primality ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/NumericalMethods/PrimeTester.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/NumericalMethods/Primality_Tests.cs))
- [X] Generate Primes using Sieve of Eratosthenes ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/NumericalMethods/PrimeGenerator.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/NumericalMethods/PrimeGenerator_Tests.cs))
- [X] Fast Exponentiation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/NumericalMethods/Exponentiation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/NumericalMethods/Exponentiation_Tests.cs))


## Geometry (in 2D)

- [X] Convex hull using Gift wrapping algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Geometry/ConvexHull.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Geometry/ConvexHull_Tests.cs))
- [X] Line intersection ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Geometry/LineIntersection.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Geometry/LineIntersection_Tests.cs))
- [X] Closest point pair ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Geometry/ClosestPointPair.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Geometry/ClosestPointPair_Tests.cs))
- [X] Check if given point inside polygon ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Geometry/PointInsidePolygon.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Geometry/PointInsidePolygon_Tests.cs))
- [X] Rectangle intersection ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Geometry/RectangleIntersection.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Geometry/RectangleIntersection_Tests.cs))
- [X] Point Rotation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Geometry/PointRotation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Geometry/PointRotation_Tests.cs))

## Bit Manipulation

- [X] Some common bit hacks ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/BitHacks.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/BitHacks_Tests.cs))
- [X] Int to Binary string ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/IntToBinary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/IntToBinary_Tests.cs))
- [X] Base conversion ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/BaseConversion.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/BaseConversion_Tests.cs))
- [X] Next Power of 2 ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/NextPowOfTwo.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/NextPowOfTwo_Tests.cs))
- [X] Check if a Number is Multiple of 3 ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/IsMultipleOfThree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/IsMultipleOfThree_Tests.cs))
- [X] Check if a number is multiple of 9 ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/IsMultipleOfNine.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/IsMultipleOfNine_Tests.cs))
- [X] Calculate Logarithm (base 2 & 10) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/CalcLogarithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/CalcLogarithm_Tests.cs))
- [X] GCD ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/GCD.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/GCD_Tests.cs))
- [X] Modulus of division by power of two ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/DivisionModulus.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/DivisionModulus_Tests.cs))
- [X] Toggle upper/lower case ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/BitAlgorithms/ToggleCase.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/BitAlgorithms/ToggleCase_Tests.cs))

## Miscellaneous

- [X] Matrix Multiplication ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms/Miscellaneous/MatrixMultiplication.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/tree/develop/Advanced.Algorithms.Tests/Miscellaneous/MatrixMultiplication_Tests.cs))
