# Correctness audit log
| path | status | note |
|------|--------|------|
| src/Advanced.Algorithms/Search/BinarySearch.cs | fixed | empty input indexed `input[0]`; guard `i > j` → `-1`; oracle vs known indices |
| src/Advanced.Algorithms/Search/BoyerMoore.cs | ok | majority oracle vs `count > n/2`; no code change |
| src/Advanced.Algorithms/Search/QuickSelect.cs | ok | kth-smallest oracle vs `sorted[k-1]`; no code change |
| src/Advanced.Algorithms/Compression/HuffmanCoding.cs | ok | codebook roundtrip + prefix-free + frequency/length sanity; no decode API |
| src/Advanced.Algorithms/Sorting/BubbleSort.cs | ok | Array.Sort oracle on empty/single/dups/reverse/sorted/random |
| src/Advanced.Algorithms/Sorting/InsertionSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/SelectionSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/ShellSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/MergeSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/QuickSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/HeapSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/CountingSort.cs | fixed | empty input overflowed `max+1` (`int.MinValue`); early return `Array.Empty` |
| src/Advanced.Algorithms/Sorting/RadixSort.cs | fixed | empty input threw on `Max()`; early return |
| src/Advanced.Algorithms/Sorting/BucketSort.cs | fixed | `bucketSize==0` on non-empty returned zeros; reject invalid size |
| src/Advanced.Algorithms/Sorting/TreeSort.cs | ok | Array.Sort oracle on unique keys; duplicates throw by design (RB tree) |
| src/Advanced.Algorithms/Binary/GCD.cs | pass | Fixed gcd(negative,0) returning negative; Euclidean oracle tests. |
| tests/Advanced.Algorithms.Tests/Binary/GCD_Tests.cs | pass | Oracle vs Euclidean over grid and edge pairs. |
| src/Advanced.Algorithms/Binary/Logarithm.cs | pass | Fixed CalcBase10LogFloor (was floor(log2)/floor(log2(10))); FP epsilon. |
| tests/Advanced.Algorithms.Tests/Binary/Logarithm_Tests.cs | pass | Oracle vs Math.Log/Log10 for 1..2000 and adversarial samples. |
| src/Advanced.Algorithms/Binary/BaseConversion.cs | pass | No impl change; whole-number round-trips OK. |
| tests/Advanced.Algorithms.Tests/Binary/BaseConversion_Tests.cs | pass | Round-trip oracle dec<->bin/hex/base3 + known literals. |
| src/Advanced.Algorithms/Numerical/PrimeTester.cs | pass | No impl change; matches known primes. |
| tests/Advanced.Algorithms.Tests/Numerical/Primality_Tests.cs | pass | Oracle vs primes <=100 and composites beyond. |
| src/Advanced.Algorithms/Numerical/PrimeGenerator.cs | pass | No impl change; sieve matches known list / IsPrime. |
| tests/Advanced.Algorithms.Tests/Numerical/PrimeGenerator_Tests.cs | pass | Oracle list to 100; cross-check IsPrime to 200; max 0/1. |
| src/Advanced.Algorithms/Numerical/Exponentiation.cs | pass | No impl change; matches Math.Pow in int range. |
| tests/Advanced.Algorithms.Tests/Numerical/Exponentiation_Tests.cs | pass | Oracle vs Math.Pow incl. negative bases. |
| src/Advanced.Algorithms/Combinatorics/Combination.cs | pass | No impl change; counts match C(n,k) / C(n+r-1,r). |
| tests/Advanced.Algorithms.Tests/Combinatorics/Combination_Tests.cs | pass | Formula oracle n,r <=7; overflow-safe binomial helper. |
| src/Advanced.Algorithms/Combinatorics/Permutation.cs | pass | No impl change; counts match P(n,r) / n^r. |
| tests/Advanced.Algorithms.Tests/Combinatorics/Permutation_Tests.cs | pass | Formula oracle n,r <=6. |
| src/Advanced.Algorithms/Combinatorics/Subset.cs | pass | No impl change; power set size 2^n and C(n,k) sizes. |
| tests/Advanced.Algorithms.Tests/Combinatorics/Subset_Tests.cs | pass | Power-set count + per-size binomial oracle n<=10. |
| src/Advanced.Algorithms/Graph/Search/DepthFirst.cs | fixed | missed disconnected components; search all starts; empty-graph guard |
| tests/Advanced.Algorithms.Tests/Graph/Search/DepthFirst_Tests.cs | pass | disconnected-component existence oracle |

| src/Advanced.Algorithms/Geometry/PointInsidePolygon.cs | fixed | ray loop used Edges.Count-1 (skipped last edge); outside-left false positive |
| tests/Advanced.Algorithms.Tests/Geometry/PointInsidePolygon_Tests.cs | pass | square vertex oracle; known inside/outside incl. left of square |
| src/Advanced.Algorithms/Graph/Search/BreadthFirst.cs | fixed | missed disconnected components; search all starts; empty-graph guard |
| tests/Advanced.Algorithms.Tests/Graph/Search/BreadthFirst_Tests.cs | pass | disconnected-component existence oracle |
| src/Advanced.Algorithms/Graph/Search/BiDirectional.cs | fixed | reverse BFS used out-edges (false paths on digraphs); use InEdges; IDiGraph API |
| tests/Advanced.Algorithms.Tests/Graph/Search/BiDirectional_Tests.cs | pass | converging A->B<-C adversarial path oracle |
| src/Advanced.Algorithms/Graph/Sort/KahnTopSort.cs | fixed | enqueued every neighbor (dupes); incomplete result on cycles; enqueue at indegree 0 + count check |
| tests/Advanced.Algorithms.Tests/Graph/Sort/KahnTopSort_Tests.cs | pass | topo vs DAG edges oracle; diamond/cycle adversarial |
| src/Advanced.Algorithms/Graph/Sort/DepthFirstTopSort.cs | fixed | cyclic graphs returned an order; visiting-set cycle throw |
| tests/Advanced.Algorithms.Tests/Graph/Sort/DepthFirstTopSort_Tests.cs | pass | topo vs DAG edges oracle; cycle throws |
| src/Advanced.Algorithms/Graph/Cycle/CycleDetection.cs | ok | DFS visiting/visited correct; no code change |
| tests/Advanced.Algorithms.Tests/Graph/Cycle/CycleDetection_Tests.cs | pass | self-loop + DAG adversarial |
| src/Advanced.Algorithms/Graph/ArticulationPoint/TarjansArticulationFinder.cs | fixed | only scanned reference component; iterate all DFS roots |
| tests/Advanced.Algorithms.Tests/Graph/ArticulationPoints/TarjansArticulation_Tests.cs | pass | disconnected triangle+path AP oracle |
| src/Advanced.Algorithms/Graph/Bridge/TarjansBridgeFinder.cs | fixed | only scanned reference component; iterate all DFS roots |
| tests/Advanced.Algorithms.Tests/Graph/Bridge/TarjansBridge_Tests.cs | pass | disconnected triangle+edge bridge oracle |
| src/Advanced.Algorithms/Graph/Connectivity/KosarajuStronglyConnected.cs | ok | hand fixture SCCs match; no code change |
| tests/Advanced.Algorithms.Tests/Graph/Connectivity/KosarajuStronglyConnected_Tests.cs | pass | {A,B}/{C} hand fixture |
| src/Advanced.Algorithms/Graph/Connectivity/TarjansStronglyConnected.cs | ok | matches Kosaraju on hand fixture; no code change |
| tests/Advanced.Algorithms.Tests/Graph/Connectivity/TarjansStronglyConnected_Tests.cs | pass | SCC set equality vs Kosaraju |
| src/Advanced.Algorithms/Graph/Connectivity/TarjansBiConnected.cs | fixed | disconnected graphs with no APs reported bi-connected; require connectivity |
| tests/Advanced.Algorithms.Tests/Graph/Connectivity/TarjansBiConnected_Tests.cs | pass | two triangles adversarial |
| src/Advanced.Algorithms/Graph/Coloring/MColorer.cs | fixed | re-entry threw on already-colored vertices; shared visited + skip if colored; greedy (may false-negative) |
| tests/Advanced.Algorithms.Tests/Graph/Coloring/MColoring_Tests.cs | pass | odd-cycle 2/3-color + uncolorable no-throw; proper-coloring check |
| src/Advanced.Algorithms/Graph/Cover/MinVertexCover.cs | fixed | only covered reference component; iterate all vertices (approx) |
| tests/Advanced.Algorithms.Tests/Graph/Cover/MinVertexCover.cs | pass | disconnected two-edge cover oracle |
| src/Advanced.Algorithms/Graph/Matching/BiPartiteMatching.cs | ok | max matching size matches known fixtures; no code change |
| tests/Advanced.Algorithms.Tests/Graph/Matching/BiPartiteMatching_Tests.cs | pass | augmenting-path size=2 oracle |
| src/Advanced.Algorithms/Graph/Matching/HopcroftKarp.cs | fixed | BFS marked wrong vertex visited; followed non-matched edges; mark current + matched-only |
| tests/Advanced.Algorithms.Tests/Graph/Matching/HopcroftKarp_Tests.cs | pass | known matching size oracles |
| src/Advanced.Algorithms/Geometry/ConvexHull.cs | fixed | empty guard; Jarvis prefers farthest collinear (drop edge midpoints) |
| tests/Advanced.Algorithms.Tests/Geometry/ConvexHull_Tests.cs | pass | oracle vs square/triangle/grid/collinear endpoints |
| src/Advanced.Algorithms/Geometry/LineIntersection.cs | fixed | parallel diagonal collinear overlap returned null; mirror V/H overlap |
| tests/Advanced.Algorithms.Tests/Geometry/LineIntersection_Tests.cs | pass | hand-case oracle cross/T/parallel/diagonal overlap |
| src/Advanced.Algorithms/Geometry/ClosestPointPair.cs | ok | matches O(n^2) brute on n<=40; no code change |
| tests/Advanced.Algorithms.Tests/Geometry/ClosestPointPair_Tests.cs | pass | brute-force oracle + duplicate distance 0 |
| src/Advanced.Algorithms/Geometry/RectangleIntersection.cs | ok | overlap/contain/disjoint/touch match hand cases; no code change |
| tests/Advanced.Algorithms.Tests/Geometry/RectangleIntersection_Tests.cs | pass | hand-case oracle |
| src/Advanced.Algorithms/Geometry/PointRotation.cs | ok | 90/180/270 match analytic; no code change |
| tests/Advanced.Algorithms.Tests/Geometry/PointRotation_Tests.cs | pass | cardinal-angle oracle |
| src/Advanced.Algorithms/Geometry/BentleyOttmann.cs | fixed | Event.CompareTo NRE via segment vs finite sweepline; use Y-at-sweep-X |
| tests/Advanced.Algorithms.Tests/Geometry/BentleyOttmann_Tests.cs | pass | pairwise LineIntersection oracle + shared-endpoint crash regression |
| src/Advanced.Algorithms/Graph/MinimumSpanningTree/Kruskals.cs | ok | MST weight matches Prim on shared graph; no code change |
| tests/Advanced.Algorithms.Tests/Graph/MinimumSpanningTree/Kruskals_Test.cs | pass | Prim weight cross-oracle (=15) |
| src/Advanced.Algorithms/Graph/MinimumSpanningTree/Prims.cs | ok | MST weight matches Kruskal; no code change |
| tests/Advanced.Algorithms.Tests/Graph/MinimumSpanningTree/Prims_Test.cs | pass | Kruskal weight cross-oracle |
| src/Advanced.Algorithms/Graph/ShortestPath/Bellman-Ford.cs | fixed | neg-cycle check was dead (`iterations < 0`); TracePath hung on cycles; extra relax pass throws |
| tests/Advanced.Algorithms.Tests/Graph/ShortestPath/BellmanFord_Tests.cs | pass | Dijkstra oracle + neg-cycle throws |
| src/Advanced.Algorithms/Graph/ShortestPath/Dijikstra.cs | ok | lengths match BF/Floyd on non-neg shared digraph/undirected; no code change |
| tests/Advanced.Algorithms.Tests/Graph/ShortestPath/Dijikstras_Tests.cs | pass | BF + Floyd cross-oracle S→T=15 |
| src/Advanced.Algorithms/Graph/ShortestPath/Floyd-Warshall.cs | ok | all-pairs match Dijkstra on undirected; no code change |
| tests/Advanced.Algorithms.Tests/Graph/ShortestPath/FloydWarshall_Tests.cs | pass | Dijkstra all-pairs oracle |
| src/Advanced.Algorithms/Graph/ShortestPath/Johnsons.cs | fixed | returned reweighted d' not d'+h(v)-h(u); skip unreachable fake paths |
| tests/Advanced.Algorithms.Tests/Graph/ShortestPath/Johnson_Tests.cs | pass | Bellman-Ford reachable-pair oracle |
| src/Advanced.Algorithms/Graph/ShortestPath/AStar.cs | fixed | heap CompareTo used h only; now f=g+h |
| tests/Advanced.Algorithms.Tests/Graph/ShortestPath/AStar_Tests.cs | pass | zero-h + admissible-h Dijkstra oracles |
| src/Advanced.Algorithms/Graph/ShortestPath/TravellingSalesman.cs | fixed | DP cache key omitted visited set → wrong tours |
| tests/Advanced.Algorithms.Tests/Graph/ShortestPath/TravellingSalesman_Tests.cs | pass | brute-force oracle n=5 × 30 trials |
| src/Advanced.Algorithms/Graph/Flow/FordFulkerson.cs | fixed | residual CreateResidualGraph threw on antiparallel edges |
| tests/Advanced.Algorithms.Tests/Graph/Flow/FordFulkerson_Tests.cs | pass | EK/PR cross-oracle + antiparallel |
| src/Advanced.Algorithms/Graph/Flow/EdmondsKarp.cs | fixed | same residual antiparallel fix |
| tests/Advanced.Algorithms.Tests/Graph/Flow/EdmondsKarp_Tests.cs | pass | FF/PR cross-oracle + antiparallel |
| src/Advanced.Algorithms/Graph/Flow/PushRelabel.cs | fixed | same residual antiparallel fix |
| tests/Advanced.Algorithms.Tests/Graph/Flow/PushRelabel_Tests.cs | pass | FF/EK cross-oracle + antiparallel |
| src/Advanced.Algorithms/Graph/Cut/MinimumCut.cs | ok | cut capacity = max flow; uses Edmonds residual (inherits fix); no code change |
| tests/Advanced.Algorithms.Tests/Graph/Cut/MinCut_Tests.cs | pass | cut capacity vs Edmonds-Karp max-flow oracle |

| src/Advanced.Algorithms/DataStructures/List/ArrayList.cs | fixed | negative index threw IndexOutOfRange; RemoveAt shifted past Length; InsertAt lacked bounds |
| tests/Advanced.Algorithms.Tests/DataStructures/Lists/ArrayList_Tests.cs | pass | List<T> random-ops oracle + negative/OOB ArgumentException |
| src/Advanced.Algorithms/DataStructures/List/SkipList.cs | fixed | Insert used Find().Equals(default) so default(T) duplicates were allowed |
| tests/Advanced.Algorithms.Tests/DataStructures/Lists/SkipList_Tests.cs | pass | default(0) dup throws; SortedSet random-ops oracle |
| src/Advanced.Algorithms/DataStructures/LinkedList/SinglyLinkedList.cs | fixed | enumerator Reset set Current=Head (skipped first); now null before-first |
| tests/Advanced.Algorithms.Tests/DataStructures/LinkedList/SinglyLinkedList_Tests.cs | pass | Reset returns first; List<T> head/tail oracle |
| src/Advanced.Algorithms/DataStructures/LinkedList/DoublyLinkedList.cs | fixed | same enumerator Reset bug |
| tests/Advanced.Algorithms.Tests/DataStructures/LinkedList/DoublyLinkedList_Tests.cs | pass | Reset returns first; List<T> oracle + Head/Tail |
| src/Advanced.Algorithms/DataStructures/LinkedList/CircularLinkedList.cs | fixed | Union overwrote Previous before splice (broke circle); enumerator Reset skipped first |
| tests/Advanced.Algorithms.Tests/DataStructures/LinkedList/CircularLinkedList_Tests.cs | pass | Union circle integrity; bag oracle; Reset |
| src/Advanced.Algorithms/DataStructures/Stack/Stack.cs | ok | Array+LinkedList backends; LIFO matches System.Stack; no code change |
| tests/Advanced.Algorithms.Tests/DataStructures/Stack_Tests.cs | pass | System.Collections.Generic.Stack push/pop/peek oracle |
| src/Advanced.Algorithms/DataStructures/Queues/Queue.cs | ok | Array+LinkedList backends; FIFO matches System.Queue; no code change |
| tests/Advanced.Algorithms.Tests/DataStructures/Queues/Queue_Tests.cs | pass | System.Collections.Generic.Queue enqueue/dequeue oracle |
| src/Advanced.Algorithms/DataStructures/Queues/PriorityQueue.cs | ok | min/max extract order matches sorted List; no code change |
| tests/Advanced.Algorithms.Tests/DataStructures/Queues/PriorityQueue_Tests.cs | pass | sorted-List extract-order oracle (asc+desc) |
